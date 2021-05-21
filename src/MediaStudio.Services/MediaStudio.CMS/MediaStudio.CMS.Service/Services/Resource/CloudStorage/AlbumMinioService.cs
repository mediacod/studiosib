using DBContext.Models;
using MediaStudioService.Core.Enums;
using MediaStudioService.Services.Audio;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace MediaStudioService.Service.ResourceService
{
    public class AlbumMinioService : ParentMinioService
    {
        private readonly AlbumPathService pathService;
        public AlbumMinioService(IConfiguration configuration, AlbumPathService _pathService)
            : base(configuration)
        {
            pathService = _pathService;
        }

        public async Task UploadCoverAsync(int IdAlbum, IFormFile cover, BucketTypes bucket)
        {
            var streamCover = CoverManager.GetResizingCover(cover, bucket);

            int idBucket = (int)bucket;
            var coverName = IFormFileManager.GetHash(streamCover) + Path.GetExtension(cover.FileName);

            var storage = pathService.GetCloud(IdAlbum, idBucket);

            if (storage?.ObjectName != null)
            {
                await RemoveFileAsync(bucket.ToString(), storage.ObjectName);
            }

            await UploadFileAsync(bucket.ToString(), coverName, streamCover);
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

                var idStorage = await pathService.AddCloudAsync(storage);
                await pathService.AddAlbumCloudAsync(idStorage, IdAlbum);
            }
        }
    }
}
