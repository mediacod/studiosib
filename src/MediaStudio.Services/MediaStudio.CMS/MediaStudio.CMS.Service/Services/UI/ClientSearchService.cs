using DBContext.Connect;
using MediaStudioService.ApiModels;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.PageModels.ClientPage;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.UI
{
    public class ClientSearchService
    {
        private readonly MediaStudioContext _postgres;
        protected readonly AlbumService _albumService;
        protected readonly PlaylistService _playlistService;
        protected readonly PerformerService _performerService;
        protected readonly TrackMinioService _trackMinioService;

        public ClientSearchService(MediaStudioContext postgres, TrackMinioService trackMinioService, AlbumService albumService, PlaylistService playlistService, PerformerService performerService)
        {
            _postgres = postgres;
            _trackMinioService = trackMinioService;
            _albumService = albumService;
            _playlistService = playlistService;
            _performerService = performerService;
        }

        public async Task<ClientSearchModel> SearchAllAsync(string filter)
        {
            var filterClause = $"%{filter}%".ToLower();
            var tracks = await GetTracksAsync(filterClause);
            var albums = await GetAlbumsAsync(filterClause);

            return new ClientSearchModel()
            {
                Tracks = tracks,
                Albums = albums,
            };
        }

        private async Task<List<SearchAlbumModel>> GetAlbumsAsync(string filterClause)
        {
            var albums = await _albumService.GetAllChecked()
                .OrderBy(album => album.Name)
                 .Where(album => EF.Functions.Like(album.Name.ToLower(), filterClause))
                 .Select(album => new SearchAlbumModel
                 {
                     IdAlbum = album.IdAlbum,
                     Name = album.Name,
                     LinkCover = _trackMinioService.MinioURL + album.AlbumStorage
                    .Where(albumCloud => albumCloud.IdAbum == album.IdAlbum
                         && albumCloud.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcoverlarge)
                    .Select(albumStorage => albumStorage.IdStorageNavigation.StaticUrl)
                    .FirstOrDefault(),
                 })
                 .Take(10)
                 .ToListAsync();
            return albums;
        }

        private async Task<List<TrackSearchResult>> GetTracksAsync(string filterClause)
        {
            var tracks = await _postgres.Track
                .AsNoTracking()
                .OrderBy(track => track.Name)
                .Where(track => !track.IsDelete
                && track.PublicationTime < DateTime.Now && EF.Functions.Like(track.Name.ToLower(), filterClause))
                .Select(track => new TrackSearchResult
                {
                    IdTrack = track.IdTrack,
                    Name = track.Name,
                    Duration = track.Duration,
                })
                .Take(10)
                .ToListAsync();

            foreach (var track in tracks)
            {
                track.Link = await _trackMinioService.BuildNewLinkAsync(track.IdTrack, BucketTypes.audio);
            }

            return tracks;
        }
    }
}
