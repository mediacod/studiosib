using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.Models.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Service.ResourceService
{
    public class TrackPathService
    {
        protected readonly MediaStudioContext postgres;
        public TrackPathService(MediaStudioContext _postgres)
        {
            postgres = _postgres;
        }
        public async Task UpdateTempLinkAsync(Storage storage)
        {
            postgres.Storage.Update(storage);
            await postgres.SaveChangesAsync();
        }

        public string GetTempLink(long idTrack, short idBucket)
        {
            return postgres.TrackStorage
                .Where(trackStorage => trackStorage.IdTrack == idTrack
                && trackStorage.IdStorageNavigation.IdBucket == idBucket
                && trackStorage.IdStorageNavigation.ValidUntil > DateTime.Now)
                .Select(trackStorage => trackStorage.IdStorageNavigation.TemporaryUrl)
                .FirstOrDefault();
        }

        public Storage GetStorage(long idTrack, short idBucket)
        {
            return postgres.TrackStorage
                .Where(trackStorage => trackStorage.IdTrack == idTrack
                && trackStorage.IdStorageNavigation.IdBucket == idBucket)
                .Select(trackStorage => trackStorage.IdStorageNavigation)
                .FirstOrDefault();
        }

        public async Task<long> AddStorageAsync(string trackName, int idBucket, string tempUrl, DateTime validUntil)
        {
            var storage = new Storage
            {
                ObjectName = trackName,
                IdBucket = idBucket,
                ValidUntil = validUntil,
            };

            postgres.Storage.Add(storage);
            await postgres.SaveChangesAsync();
            return storage.IdStorage;
        }

        public Storage TryFindStorageByAudio(string audioName)
        {
            return postgres.Storage
                    .AsNoTracking()
                    .Where(storage => storage.ObjectName == audioName)
                    .FirstOrDefault();
        }

        public Track TryFindTrackByStorage(long idStorage)
        {
            return postgres.TrackStorage
                    .AsNoTracking()
                    .Where(trackStorage => trackStorage.IdStorage == idStorage)
                    .Select(trackStorage => trackStorage.IdTrackNavigation)
                    .FirstOrDefault();
        }

        public bool TrackStorageExist(long idStorage)
        {
            return postgres.Storage
                .Any(storage => storage.IdStorage == idStorage);
        }

        public Storage GetTrackStorage(long idStorage)
        {
            return postgres.Storage
                .Include(st => st.IdBucketNavigation)
                .FirstOrDefault(storage => storage.IdStorage == idStorage);
        }

        public void AddPath(InputTrack inputTrack)
        {

            postgres.TrackStorage.Add(new TrackStorage
            {
                IdTrack = inputTrack.IdTrack,
                IdStorage = inputTrack.IdStorage,
            });


            if (inputTrack.IdStorageHQ.HasValue)
                postgres.TrackStorage.Add(new TrackStorage
                {
                    IdTrack = inputTrack.IdTrack,
                    IdStorage = inputTrack.IdStorageHQ.Value,
                });

            postgres.SaveChanges();
        }
    }
}
