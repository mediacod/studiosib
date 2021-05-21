using DBContext.Connect;
using MediaStudioService.Core.Classes;
using MediaStudioService.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;
using MediaStudio.Classes.MyException;

namespace MediaStudioService.Service.ResourceService
{
    public class TrackMinioService : ParentMinioService
    {
        private readonly TrackPathService pathService;
        protected readonly MediaStudioContext postgres;
        public TrackMinioService(IConfiguration configuration, TrackPathService _pathService, MediaStudioContext _postgres)
            : base(configuration)
        {
            pathService = _pathService;
            postgres = _postgres;
        }

        public async Task<string> BuildNewLinkAsync(long idTrack, BucketTypes bucketType)
        {
            var idBucket = (short)bucketType;
            var storage = pathService.GetStorage(idTrack, idBucket);
            var tempLink = await GetTemporaryURLAsync(bucketType.ToString(), storage.ObjectName);
            storage.TemporaryUrl = tempLink;
            await pathService.UpdateTempLinkAsync(storage);
            return tempLink;
        }

        public async Task<long> UnloadAudioAsync(IFormFile audio, BucketTypes bucket)
        {
            int idBucket = (int)bucket;
            var trackName = IFormFileManager.GetFullName(audio);

            if (!FormatValidator.IsAudioFormat(audio))
                throw new MyBadRequestException($"Ошибка! Непподерживаемый формат аудиофайла!");

            var storage = pathService.TryFindStorageByAudio(trackName);
            if (storage != null)
            {
                var track = pathService.TryFindTrackByStorage(storage.IdStorage);
                if (track?.IsDelete == false)
                {
                    var albumName = postgres.TrackToAlbum
                        .Where(trToAlbum => trToAlbum.IdTrack == track.IdTrack)
                        .Select(trToAlbum => trToAlbum.IdAlbumNavigation.Name)
                        .FirstOrDefault();
                    throw new MyBadRequestException($"Загружаемый аудиофайл уже принадлежит альбому:  {albumName!}");
                }
                else
                {
                    return storage.IdStorage;
                }
            }

            await UploadFileAsync(bucket.ToString(), trackName, audio.OpenReadStream());

            var tempUrl = await GetTemporaryURLAsync(bucket.ToString(), trackName);
            var validUntil = DateTime.Now.AddDays(-1).AddSeconds(minio.UrlLifetime).ToLocalTime();

            return await pathService.AddStorageAsync(trackName, idBucket, tempUrl, validUntil);
        }

        public bool AudioStorageExist(long idStorage)
        {
            var storage = pathService.GetTrackStorage(idStorage);
            return ExistsFile(storage.IdBucketNavigation.NameBucket, storage.ObjectName);
        }
    }
}
