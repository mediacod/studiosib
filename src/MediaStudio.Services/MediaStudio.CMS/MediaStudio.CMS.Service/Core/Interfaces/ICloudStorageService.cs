using System.IO;
using System.Threading.Tasks;

namespace MediaStudioService.Service.ResourceService
{
    public interface ICloudStorageService<T>
    {
        Task UploadFileAsync(T nameBucket, T objectName, Stream fileStream);
        Task<string> GetTemporaryURLAsync(T nameBucket, T objectName);
        Task RemoveFileAsync(T nameBucket, T objectName);
        bool ExistsFile(T nameBucket, T objectName);
    }
}
