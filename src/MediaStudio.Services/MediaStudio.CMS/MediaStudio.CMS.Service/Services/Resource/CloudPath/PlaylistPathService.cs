using DBContext.Connect;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MediaStudioService.Service.ResourceService
{
    public class PlaylistPathService
    {
        protected readonly MediaStudioContext postgres;
        public PlaylistPathService(MediaStudioContext _postgres)
        {
            postgres = _postgres;
        }

        internal bool CheckCoverExist(long idPlaylist, int idBucket)
        {
            return postgres.PlaylistStorage
                .Any(playlist => playlist.IdPlaylist == idPlaylist
                && playlist.IdStorageNavigation.IdBucket == idBucket);
        }


        internal Storage GetCloud(long idPlaylist, int idBucket)
        {
            return postgres.PlaylistStorage
                .Where(playlist => playlist.IdPlaylist == idPlaylist && playlist.IdStorageNavigation.IdBucket == idBucket)
                .Select(cloud => cloud.IdStorageNavigation)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public void UpdateCloud(Storage storage)
        {
            postgres.Storage.Update(storage);
            postgres.SaveChanges();
        }


        internal long AddCloud(Storage storage, long idPlaylist)
        {
            postgres.Storage.Add(storage);
            postgres.SaveChanges();

            var playlistCloud = new PlaylistStorage
            {
                IdPlaylist = idPlaylist,
                IdStorage = storage.IdStorage
            };

            postgres.PlaylistStorage.Add(playlistCloud);

            postgres.SaveChanges();
            return playlistCloud.IdPlaylistStorage;
        }
    }
}
