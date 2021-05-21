using Microsoft.Extensions.Configuration;
using Minio;
using System;

namespace MediaStudioService.Service.ResourceService
{
    public class MinioBuilder
    {
        public static Minio Build(IConfiguration configuration)
        {
            // загружаем данные 
            string minioURL = configuration.GetSection("Minio").GetSection("MINIO_URL").Value;
            string accessKey = configuration.GetSection("Minio").GetSection("MINIO_ACCESS_KEY").Value;
            string secretKey = configuration.GetSection("Minio").GetSection("MINIO_SECRET_KEY").Value;
            var lifeTimeObject = configuration.GetSection("Minio").GetSection("MINIO_URL_LIFETIME").Value;
            var withSSL = configuration.GetSection("Minio").GetSection("USE_SSL").Value;
            var useSSL = int.Parse(withSSL) == 1;

            if (!int.TryParse(lifeTimeObject, out int urlLifetime))
                throw new InvalidOperationException($"Ошибка при получение время жизни ссылки из json");


            return new Minio
            {
                Provider = useSSL
                            ? new MinioClient(minioURL, accessKey, secretKey).WithSSL()
                            : new MinioClient(minioURL, accessKey, secretKey),
                UrlLifetime = urlLifetime,
                URL = useSSL
                        ? "https://" + minioURL
                        : "http://" + minioURL,
            };
        }
    }
}
