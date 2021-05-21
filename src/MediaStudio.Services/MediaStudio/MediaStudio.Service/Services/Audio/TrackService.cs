using DBContext.Connect;
using MediaStudio.Classes.MyException;
using MediaStudioService.Core;
using MediaStudioService.Core.Enums;
using MediaStudioService.ModelBulder;
using MediaStudioService.Models.Input;
using MediaStudioService.Service.ResourceService;
using MediaStudioService.Services;
using MediaStudioService.Services.audit;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService
{
    public class TrackService
    {
        private readonly MediaStudioContext postgres;
        private readonly TrackBuilderService bulder;
        private readonly AuditTrackService audit;
        private readonly TrackMinioService minioService;
        private readonly TrackAlbumService trackAlbum;
        private readonly TrackPathService pathService;

        public TrackService(
            MediaStudioContext _postgres,
            AuditTrackService _audit,
            TrackMinioService _minioService,
            TrackAlbumService _trackAlbum,
            TrackPathService _pathService,
            TrackBuilderService _bulder)
        {
            audit = _audit;
            postgres = _postgres;
            minioService = _minioService;
            trackAlbum = _trackAlbum;
            pathService = _pathService;
            bulder = _bulder;
        }

        public async Task<string> DeleteAsync(long idTrack, string executorLogin)
        {
            CheckTrackExists(idTrack);

            var track = postgres.Track.Find(idTrack);

            var albumName = postgres.TrackToAlbum
                .Where(trackToAlbum => trackToAlbum.IdTrack == idTrack)
                .Select(trackToAlbum => trackToAlbum.IdAlbumNavigation.Name)
                .FirstOrDefault();

            audit.Add(LogOperaion.Удаление, track.Name, executorLogin, oldValue: $"Альбом : {albumName}");

            var trackToAlbums = postgres.TrackToAlbum
                .Where(trackAlbum => trackAlbum.IdTrack == idTrack)
                .AsNoTracking()
                .AsEnumerable();

            postgres.TrackToAlbum.RemoveRange(trackToAlbums);

            track.IsDelete = true;
            track.Name = $"Deleted -  {track.Name} - {DateTime.Now}";

            await postgres.SaveChangesAsync();
            audit.MarkSucces();
            return $"Трек {track.Name} успешно удален!";
        }

        public string Rename(long idTrack, string nameTrack, string executorLogin)
        {
            CheckTrackExists(idTrack);
            var track = postgres.Track.Find(idTrack);
            audit.Add(LogOperaion.Изменение, track.Name, executorLogin, $"name : {track.Name} => {nameTrack}", track.IdTrack);
            track.Name = nameTrack;
            postgres.SaveChanges();
            audit.MarkSucces();
            return $"Трек {track.Name} успешно переименован!";
        }

        public string UpdateOrders(TrackToOrderCollection orderClollection, string executorLogin)
        {
            foreach (var orderToTrack in orderClollection.TrackToOrderList)
            {
                UpdateOrder(orderToTrack, executorLogin);
            }
            return $"Порядок треков успешно изменен";
        }

        public string UpdateOrder(InputTrackToOrder trackToOrder, string executorLogin)
        {
            var idTrack = trackToOrder.IdTrack;
            var idOrder = trackToOrder.IdOrder;

            CheckTrackExists(idTrack);
            var track = postgres.Track.Find(idTrack);
            audit.Add(LogOperaion.Изменение, track.Name, executorLogin, oldValue: $"idOrder : {track.AlbumOrder} => {idOrder}");
            track.AlbumOrder = idOrder;
            postgres.SaveChanges();
            audit.MarkSucces();
            return $"Порядок трека {track.Name} успешно изменен";
        }

        public async Task<long> UnloadAsync(IFormFile audio, bool selectedHQ, string executorLogin)
        {
            audit.Add(LogOperaion.Загрузка, audio.FileName, executorLogin, oldValue: $"Хеш : {IFormFileManager.GetHash(audio)}");
            var idStorage = await minioService.UnloadAudioAsync(audio, selectedHQ ? BucketTypes.audiohq : BucketTypes.audio);
            audit.MarkSucces();
            return idStorage;
        }

        public long Add(InputTrack inputTrack, string executorLogin)
        {
            // проверяем что пришли валидные данные
            CheckValidInputTrack(inputTrack);

            var albumName = postgres.Album.Find(inputTrack.IdAlbum).Name;
            audit.Add(LogOperaion.Добавление, inputTrack.Name, executorLogin, oldValue: $"Альбом : {albumName}");

            // добавляем  запись_трек в БД
            var track = bulder.BuldPostgresTrack(inputTrack);
            postgres.Add(track);
            postgres.SaveChanges();

            inputTrack.IdTrack = track.IdTrack;

            //добавляем ссылку на хранилище
            pathService.AddPath(inputTrack);

            // добавляем ссылку трека на альбом
            trackAlbum.AddRecord(inputTrack.IdAlbum, track.IdTrack);
            audit.MarkSucces(track.IdTrack);
            return track.IdTrack;
        }

        private void CheckValidInputTrack(InputTrack inputTrack)
        {
            if (inputTrack.IdStorage == 0)
                throw new MyBadRequestException($"Ошибка! Отсуствует ссылка на файл для трека {inputTrack.Name}!");

            if (!pathService.TrackStorageExist(inputTrack.IdStorage))
                throw new MyBadRequestException($"Ошибка! В БД отсуствует ссылка на файл трека {inputTrack.Name} в папке {MinioBuckets.audio}!");


            if (!minioService.AudioStorageExist(inputTrack.IdStorage))
                throw new MyNotFoundException($"Ошибка! Не найден аудиофайл для трека {inputTrack.Name} в папке {MinioBuckets.audio}!");

            if (inputTrack.IdStorageHQ.HasValue)
            {
                if (!minioService.AudioStorageExist(inputTrack.IdStorageHQ.Value))
                    throw new MyNotFoundException($"Ошибка! Не найден аудиофайл для трека {inputTrack.Name} в папке {MinioBuckets.audioHQ}!");
            }

            if (!postgres.Album.Any(e => e.IdAlbum == inputTrack.IdAlbum))
                throw new MyNotFoundException($"Ошибка! В БД отсуствует альбом с ID {inputTrack.IdAlbum}!");
        }

        private void CheckTrackExists(long idTrack)
        {
            if (!postgres.Track.Any(e => e.IdTrack == idTrack))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден трек с id {idTrack}!");
        }
    }
}
