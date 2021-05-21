using DBContext.Connect;
using System;
using System.Collections.Generic;
using MediaStudioService.ApiModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MediaStudioService.ModelBulder;
using System.Linq;
using MediaStudioService.Models.Input;
using MediaStudioService.Services.audit;
using MediaStudioService.Core.Enums;
using DBContext.Models;
using MediaStudioService.Service.ResourceService;
using SixLabors.ImageSharp;
using MediaStudioService.Core.Classes;
using MediaStudio.Classes.MyException;
using MediaStudioService.Models.PageModels.ClientPage;

namespace MediaStudioService
{
    public class AlbumService
    {
        private readonly MediaStudioContext postgres;
        private readonly AlbumBuilderService bulder;
        private readonly AuditAlbumService audit;
        private readonly AlbumMinioService minioService;

        public AlbumService(MediaStudioContext _postgres, AuditAlbumService _audit, AlbumMinioService _minioService, AlbumBuilderService _bulder)
        {
            audit = _audit;
            postgres = _postgres;
            minioService = _minioService;
            bulder = _bulder;
        }

        public IQueryable<Album> GetAll()
        {
            return postgres.Album
                .AsNoTracking()
                .Where(album => !album.IsDelete)
                .AsQueryable();
        }

        public IQueryable<Album> GetAllChecked()
        {
            return postgres.Album
                .AsNoTracking()
                .Where(album => !album.IsDelete
                             && album.IsChecked
                             && album.PublicationTime <= DateTime.Now)
                .AsQueryable();
        }

        public async Task<SearchAlbumModel> GetAlbumByTrack(long idTrack)
        {
            if (!postgres.TrackToAlbum.Any(e => e.IdTrack == idTrack))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден трек альбома с idTrack {idTrack}!");

            return await postgres.TrackToAlbum
                .AsNoTracking()
                .Where(trToAlbum => trToAlbum.IdTrack == idTrack)
                .Select(trToAlbum => new SearchAlbumModel
                {
                    IdAlbum = trToAlbum.IdAlbumNavigation.IdAlbum,
                    Name = trToAlbum.IdAlbumNavigation.Name,
                    LinkCover = minioService.MinioURL + trToAlbum.IdAlbumNavigation.AlbumStorage
                    .Where(albumCloud => albumCloud.IdAbum == trToAlbum.IdAlbumNavigation.IdAlbum
                         && albumCloud.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium)
                    .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl)
                    .FirstOrDefault(),
                }).FirstOrDefaultAsync();
        }

        public async Task<PageAlbum> GetAsync(int idAlbum, bool isHighQuality)
        {
            CheckAlbumExists(idAlbum);

            var pageAlbum = await GetAllChecked()
                .Where(album => album.IdAlbum == idAlbum)
                .Select(album => new PageAlbum
                {
                    IdAlbum = album.IdAlbum,
                    Name = album.Name,
                    Duration = album.Duration,
                    IdTypeAudio = album.IdTypeAudio,
                    HighQualityExist = album.HighQualityExist,
                    IsChecked = album.IsChecked,
                    LinkCover = minioService.MinioURL + album.AlbumStorage
                    .Where(albumCloud => albumCloud.IdAbum == album.IdAlbum
                    && albumCloud.IdStorageNavigation.IdBucket == (short)BucketTypes.albumcoverlarge)
                    .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();

            if (pageAlbum.LinkCover == null)
            {
                pageAlbum.LinkCover = minioService.MinioURL + postgres.AlbumStorage
                    .AsNoTracking()
                    .Where(albumStorage => albumStorage.IdAbum == idAlbum
                    && albumStorage.IdStorageNavigation.IdBucket == (short)BucketTypes.albumcovermedium)
                    .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl)
                    .FirstOrDefault();
            }

            // получаем только опубликованные треки
            var bucketName = isHighQuality ? BucketTypes.audiohq : BucketTypes.audio;
            pageAlbum.Tracks = await bulder.GetTracksAsync(idAlbum, bucketName, false);

            return pageAlbum;
        }

        public async Task<PageAlbum> GetAdminAsync(int idAlbum, bool isHighQuality)
        {
            CheckAlbumExists(idAlbum);

            var pageAlbum = await GetAll()
                .Where(album => album.IdAlbum == idAlbum)
                .Select(album => new PageAlbum
                {
                    IdAlbum = album.IdAlbum,
                    Name = album.Name,
                    Duration = album.Duration,
                    IdTypeAudio = album.IdTypeAudio,
                    HighQualityExist = album.HighQualityExist,
                    IsChecked = album.IsChecked,
                    LinkCover = minioService.MinioURL + album.AlbumStorage
                    .Where(albumCloud => albumCloud.IdAbum == album.IdAlbum
                    && albumCloud.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcoverlarge)
                    .Select(s => s.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();

            // получаем все треки (опубликованные и нет)
            var bucketName = isHighQuality ? BucketTypes.audiohq : BucketTypes.audio;
            pageAlbum.Tracks = await bulder.GetTracksAsync(idAlbum, bucketName, true);

            return pageAlbum;
        }

        public async Task<List<PageAlbum>> GetOutsideSectionAlbumsAsync(InputOutsideSection inputOutsideSection)
        {
            var idSection = inputOutsideSection.IdSection;

            if (!postgres.Section.Any(section => section.IdSection == idSection))
                throw new MyNotFoundException($"Ошибка! В базе даных отсуствует секция с id {idSection}");

            var idSectionAlbums = await postgres.SectionAlbum
                .Where(sectionAlbum => sectionAlbum.IdSection == idSection)
                .Select(sectionAlbum => sectionAlbum.IdAlbum)
                .ToListAsync();

            return await GetAll()
                .Where(a => a.IsChecked
                        && a.IdTypeAudio == inputOutsideSection.idTypeAudio
                        && !idSectionAlbums.Contains(a.IdAlbum))
                .Select(album => new PageAlbum
                {
                    LinkCover = minioService.MinioURL + album.AlbumStorage.Where(w => w.IdAbum == album.IdAlbum
                        && w.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium)
                        .Select(s => s.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                    IdAlbum = album.IdAlbum,
                    Name = album.Name,
                    Duration = album.Duration,
                    IdTypeAudio = album.IdTypeAudio,
                })
                .ToListAsync();
        }
        public async Task<List<PageAlbum>> GetListAdminAsync(bool checkedStatus)
        {
            return await GetAll()
                .Where(a => a.IsChecked == checkedStatus)
                .Select(album => new PageAlbum
                {
                    LinkCover = minioService.MinioURL + album.AlbumStorage.Where(w => w.IdAbum == album.IdAlbum
                        && w.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium)
                        .Select(s => s.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                    IdAlbum = album.IdAlbum,
                    Name = album.Name,
                    Duration = album.Duration,
                    IdTypeAudio = album.IdTypeAudio,
                    HighQualityExist = album.HighQualityExist,
                    IsChecked = album.IsChecked,
                })
                .ToListAsync();
        }

        public int Create(InputAlbum inputAlbum, string executorLogin)
        {
            audit.Add(LogOperaion.Добавление, inputAlbum.Name, executorLogin);

            CheckValidAlbum(inputAlbum);

            var album = bulder.BuldPostgresAlbum(inputAlbum);

            postgres.Add(album);
            postgres.SaveChanges();

            audit.MarkSucces(album.IdAlbum);
            return album.IdAlbum;
        }

        public string Rename(int idAlbum, string newNameAlbum, string executorLogin)
        {
            var album = GetAlbumByIdAsync(idAlbum).Result;
            audit.Add(LogOperaion.Изменение, album.Name, executorLogin, oldValue: $"Имя Альбома :{album.Name} => {newNameAlbum}");
            album.Name = newNameAlbum;
            postgres.Update(album);
            postgres.SaveChanges();
            audit.MarkSucces();
            return "Албом успешно переименован!";
        }

        public async Task<string> UpdateAlbumCover(InputCoverAlbum inputAlbum, string executorLogin)
        {
            // получаем альбом из БД
            var album = await GetAlbumByIdAsync(inputAlbum.IdAlbum);

            CheckInputCover(inputAlbum);

            audit.Add(LogOperaion.Изменение, album.Name, executorLogin, oldValue: $"Обьект:Обложка альбома, idAlbum:{inputAlbum.IdAlbum}");

            await minioService.UploadCoverAsync(inputAlbum.IdAlbum, inputAlbum.LargeCover, BucketTypes.albumcoverlarge);
            await minioService.UploadCoverAsync(inputAlbum.IdAlbum, inputAlbum.LargeCover, BucketTypes.albumcovermedium);

            audit.MarkSucces();
            return $"Обложки альбома успешно обновлены";
        }

        private void CheckInputCover(InputCoverAlbum cover)
        {
            if (!FormatValidator.IsImageFormat(cover.LargeCover))
                throw new MyBadRequestException($"Ошибка! Непподерживаемый формат изображения!");

            // проверяем на null
            if (cover.LargeCover == null)
            {
                throw new MyBadRequestException($"Ошибка! Отсуствует обложка альбома!");
            }

            // проверяем размер файла
            if (cover.LargeCover.Length < 20480)
                throw new MyBadRequestException($"Ошибка! Размер файла меньше 20480 байт!");

            if (cover.LargeCover.Length > 15728640)
                throw new MyBadRequestException($"Ошибка! Размер файла превышает 15728640 байт!");

            // проверяем разрешение картинки
            using var image = Image.Load(cover.LargeCover.OpenReadStream());

            if (image.Width < 250)

                throw new MyBadRequestException($"Ошибка! Горизонтальное разрешение меньше минимального допустимого значения (250) !");

            if (image.Width > 2000)
                throw new MyBadRequestException($"Ошибка! Горизонтальное разрешение больше максимально допустимого значения (2000) !");

            if (image.Height < 250)
                throw new MyBadRequestException($"Ошибка! Вертикальное разрешение меньше минимального допустимого значения (250) !");

            if (image.Height > 2000)
                throw new MyBadRequestException($"Ошибка! Вертикальное разрешение больше максимально допустимого значения (2000) !");
        }

        public async Task<string> Delete(int idAlbum, string executorLogin)
        {
            var album = GetAlbumByIdAsync(idAlbum).Result;

            audit.Add(LogOperaion.Удаление, album.Name, executorLogin, oldValue: $"idAlbum : {idAlbum}");

            var tracks = await postgres.TrackToAlbum.
                Where(trackToAlbum => trackToAlbum.IdAlbum == idAlbum)
                .Select(trackToAlbum => trackToAlbum.IdTrackNavigation)
                .ToListAsync();

            foreach (var track in tracks)
            {
                track.IsDelete = true;
                track.Name = $"Deleted -  {track.Name} - {DateTime.Now}";
            }

            await postgres.SaveChangesAsync();

            album.IsDelete = true;
            album.Name = $"Deleted - {album.Name} - {DateTime.Now}";
            await postgres.SaveChangesAsync();

            audit.MarkSucces();
            return $"Альбом {album.Name} с id {idAlbum} успешно удален!";
        }

        public async Task<string> SetCheckedStatusAsync(int idAlbum, bool isChecked, string executorLogin)
        {
            var album = GetAlbumByIdAsync(idAlbum).Result;
            audit.Add(LogOperaion.Изменение_статуса_публикации, album.Name, executorLogin, oldValue: $"Статус Альбома :{album.IsChecked} => {isChecked}");
            album.IsChecked = isChecked;
            postgres.Update(album);
            await postgres.SaveChangesAsync();
            audit.MarkSucces();
            return "Статус альбома успешно изменен!";
        }

        private async Task<Album> GetAlbumByIdAsync(int idAlbum)
        {
            //проверяем, есть ли альбом с таким id
            CheckAlbumExists(idAlbum);

            ///получаем альбом с заданным id из БД
            return await postgres.Album.FindAsync(idAlbum);
        }

        private void CheckAlbumExists(int id)
        {
            if (!postgres.Album.Any(e => e.IdAlbum == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден альбом с id {id}!");
        }

        private bool AlbumExists(string nameAlbum)
        {
            return postgres.Album.Any(e => e.Name == nameAlbum);
        }

        private void CheckValidAlbum(InputAlbum inputAlbum)
        {
            if (!postgres.TypeAudio.Any(typeAudio => typeAudio.IdTypeAudio == inputAlbum.IdTypeAudio))
                throw new MyBadRequestException($"Ошибка! В базе даных отсуствует тип аудио {inputAlbum.IdTypeAudio}");

            if (string.IsNullOrEmpty(inputAlbum.Name))
                throw new MyBadRequestException($"Не задано имя альбома!");

            //если такой альбом есть в БД, вызываем Exception
            if (AlbumExists(inputAlbum.Name))
                throw new MyBadRequestException($"Ошибка! В базе данных уже существует альбом с именем {inputAlbum.Name}!");
        }
    }
}
