using DBContext.Models;
using MediaStudioService.Core.Classes;
using MediaStudioService.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace MediaStudioService.Service.ResourceService

{
    public class PlaylistMinioService : ParentMinioService
    {
        private readonly PlaylistPathService pathService;
        public PlaylistMinioService(IConfiguration configuration, PlaylistPathService _pathService)
            : base(configuration)
        {
            pathService = _pathService;
        }

        public async Task UploadCoverAsync(long idPlaylist, IFormFile cover, BucketTypes bucket)
        {
            if (!FormatValidator.IsImageFormat(cover))
                throw new InvalidOperationException($"Ошибка! Непподерживаемый формат изображения!");

            int idBucket = (int)bucket;
            var coverName = IFormFileManager.GetFullName(cover);
            var storage = pathService.GetCloud(idPlaylist, idBucket);

            if (storage?.ObjectName != null)
            {
                await RemoveFileAsync(bucket.ToString(), coverName);
            }

            await UploadFileAsync(bucket.ToString(), coverName, cover.OpenReadStream());
            var staticUrl = BuildObjectURL(bucket.ToString(), coverName);

            if (storage != null)
            {
                storage.ObjectName = coverName;
                storage.IdBucket = idBucket;
                storage.StaticUrl = staticUrl;
                pathService.UpdateCloud(storage);
            }
            else
            {
                storage = new Storage
                {
                    ObjectName = coverName,
                    IdBucket = idBucket,
                    StaticUrl = staticUrl,
                };

                pathService.AddCloud(storage, idPlaylist);
            }
        }
    }
}
