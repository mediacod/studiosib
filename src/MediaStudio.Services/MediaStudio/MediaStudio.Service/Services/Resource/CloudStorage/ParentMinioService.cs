using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using System;

namespace MediaStudioService.Service.ResourceService
{
    public class ParentMinioService : ICloudStorageService<string>
    {
        protected readonly Minio minio;
        public string MinioURL { get; private set; }
        public ParentMinioService(IConfiguration configuration)
        {
            minio = MinioBuilder.Build(configuration);
            MinioURL = minio.URL;
        }
        public string BuildObjectURL(string nameBucket, string objectName)
        {
            return $"{nameBucket}/{objectName}";
        }

        public async Task<string> GetTemporaryURLAsync(string nameBucket, string objectName)
        {
            return await minio.Provider.PresignedGetObjectAsync(nameBucket, objectName, minio.UrlLifetime);
        }

        public async Task UploadFileAsync(string nameBucket, string objectName, Stream fileStream)
        {
            await minio.Provider.PutObjectAsync(nameBucket, objectName, fileStream, fileStream.Length);
        }

        public async Task RemoveFileAsync(string nameBucket, string objectName)
        {
            await minio.Provider.RemoveObjectAsync(nameBucket, objectName);
        }

        public bool ExistsFile(string nameBucket, string objectName)
        {
            try
            {
                var file = minio.Provider.StatObjectAsync(nameBucket, objectName).Result;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
