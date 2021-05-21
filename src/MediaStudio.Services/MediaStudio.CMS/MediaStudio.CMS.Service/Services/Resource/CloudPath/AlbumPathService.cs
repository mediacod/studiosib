using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Service.ResourceService
{
    public class AlbumPathService
    {
        protected readonly MediaStudioContext postgres;
        public AlbumPathService(MediaStudioContext _postgres)
        {
            postgres = _postgres;
        }

        internal bool CheckCoverExist(int idAlbum, int idBucket)
        {
            return postgres.AlbumStorage
                .Any(album => album.IdAbum == idAlbum
                && album.IdStorageNavigation.IdBucket == idBucket);
        }


        internal Storage GetCloud(int idAlbum, int idBucket)
        {
            return postgres.AlbumStorage
                .Where(album => album.IdAbum == idAlbum && album.IdStorageNavigation.IdBucket == idBucket)
                .Select(cloud => cloud.IdStorageNavigation)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void UpdateCloud(Storage storage)
        {
            postgres.Storage.Update(storage);
            postgres.SaveChanges();
        }


        internal async Task<long> AddCloudAsync(Storage storage)
        {
            postgres.Storage.Add(storage);
            await postgres.SaveChangesAsync();
            return storage.IdStorage;
        }

        internal async Task<long> AddAlbumCloudAsync(long idStorage, int idAlbum)
        {
            var albumCloud = new AlbumStorage
            {
                IdAbum = idAlbum,
                IdStorage = idStorage,
            };

            postgres.AlbumStorage.Add(albumCloud);

            await postgres.SaveChangesAsync();
            return albumCloud.IdAlbumStorage;
        }
    }
}
