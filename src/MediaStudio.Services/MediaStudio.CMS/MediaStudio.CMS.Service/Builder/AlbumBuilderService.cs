using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.ApiModels;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.Input;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.ModelBulder
{
    public class AlbumBuilderService
    {
        private readonly MediaStudioContext postgres;
        private readonly TrackMinioService minioService;

        public AlbumBuilderService(MediaStudioContext _postgres, TrackMinioService _minioService)
        {
            postgres = _postgres;
            minioService = _minioService;
        }


        private IQueryable<TrackToAlbum> GetAllTrack()
        {
            return postgres.TrackToAlbum
                .AsNoTracking()
                .Where(trackToAlbum => !trackToAlbum.IdTrackNavigation.IsDelete);
        }

        // получаем только опубликованные треки
        private IQueryable<TrackToAlbum> GetPublichedTrack()
        {
            return postgres.TrackToAlbum
                .AsNoTracking()
                .Where(trackToAlbum => !trackToAlbum.IdTrackNavigation.IsDelete
                && trackToAlbum.IdTrackNavigation.PublicationTime < DateTime.Now);
        }

        public Album BuldPostgresAlbum(InputAlbum inputAlbum)
        {
            var album = new Album()
            {
                Name = inputAlbum.Name,
                IdTypeAudio = inputAlbum.IdTypeAudio,
                ReleaseYear = inputAlbum.ReleaseYear,
                HighQualityExist = inputAlbum.HighQualityExist,
            };
            return album;
        }

        // получаем все треки заданного альбома для админки
        public async Task<List<PageTrack>> GetTracksAsync(int IdAlbum, BucketTypes audioBucket, bool getAllTrack)
        {
            var idBucket = (short)audioBucket;

            var trackQueryClause = getAllTrack
                ? GetAllTrack()
                : GetPublichedTrack();

            var listPageTrack = await trackQueryClause
                .Where(trackToAlbum => trackToAlbum.IdAlbum == IdAlbum)
                .Select(trackAlbum => new PageTrack
                {
                    IdTrack = trackAlbum.IdTrack,
                    Name = trackAlbum.IdTrackNavigation.Name,
                    AlbumOrder = trackAlbum.IdTrackNavigation.AlbumOrder,
                    Duration = trackAlbum.IdTrackNavigation.Duration,
                    Link = trackAlbum.IdTrackNavigation.TrackStorage
                        .Where(tr => tr.IdTrack == trackAlbum.IdTrack
                         && tr.IdStorageNavigation.IdBucket == idBucket
                         && tr.IdStorageNavigation.ValidUntil > DateTime.Now)
                        .Select(trackStorage => trackStorage.IdStorageNavigation.TemporaryUrl)
                        .FirstOrDefault(),
                })
                .OrderBy(pageTrack => pageTrack.AlbumOrder)
                .ToListAsync();

            foreach (var pageTrack in listPageTrack
                .Where(pageTrack => pageTrack.Link == null))
            {
                pageTrack.Link = await minioService.BuildNewLinkAsync(pageTrack.IdTrack, audioBucket);
            }

            return listPageTrack;
        }
    }
}
