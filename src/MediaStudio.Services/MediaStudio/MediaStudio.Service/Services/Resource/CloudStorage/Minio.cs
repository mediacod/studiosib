using Minio;

namespace MediaStudioService.Service.ResourceService
{
    public class Minio
    {
        public MinioClient Provider { get; set; }

        public int UrlLifetime { get; set; }

        /// <summary>
        ///   требует описанние протокола перед URL
        /// </summary>
        public string URL { get; set; }
    }
}
