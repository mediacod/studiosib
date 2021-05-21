using DBContext.Connect;
using MediaStudioService.ApiModels;
using MediaStudioService.Core.Enums;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Builder.PageModelBuilder
{
    public class PlaylistBuilderService
    {
        private readonly MediaStudioContext postgres;
        private readonly TrackMinioService _trackMinioService;

        public PlaylistBuilderService(MediaStudioContext _postgres, TrackMinioService trackMinioService)
        {
            postgres = _postgres;
            _trackMinioService = trackMinioService;
        }

        // получаем все треки заданного плейлиста
        public async Task<List<PagePlaylistTrack>> GetListTracksAsync(long idPlaylist, BucketTypes audioBucket)
        {
            var idBucket = (short)audioBucket;

            var listPageTrack = await postgres.TrackToPlaylist
                .AsNoTracking()
                .Where(trackPlaylist => trackPlaylist.IdPlaylist == idPlaylist
                && trackPlaylist.IdTrackNavigation.PublicationTime < DateTime.Now)
                .Select(trackPlaylist => new PagePlaylistTrack
                {
                    IdTrackToPlaylist = trackPlaylist.IdTrackToPlaylist,
                    IdTrack = trackPlaylist.IdTrack,
                    Name = trackPlaylist.IdTrackNavigation.Name,
                    Order = trackPlaylist.Order,
                    Duration = trackPlaylist.IdTrackNavigation.Duration,
                    Link = trackPlaylist.IdTrackNavigation.TrackStorage
                        .Where(tr => tr.IdTrack == trackPlaylist.IdTrack && tr.IdStorageNavigation.ValidUntil > DateTime.Now && tr.IdStorageNavigation.IdBucket == idBucket)
                        .Select(tr => tr.IdStorageNavigation.TemporaryUrl)
                        .FirstOrDefault(),
                })
                .OrderBy(t => t.Order)
                .ToListAsync();

            foreach (var pageTrack in listPageTrack.Where(pageTrack => pageTrack.Link == null))
            {
                pageTrack.Link = await _trackMinioService.BuildNewLinkAsync(pageTrack.IdTrack, audioBucket);
            }

            return listPageTrack;
        }
    }
}
