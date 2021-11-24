using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Builder.PageModelBuilder;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.Input;
using MediaStudioService.Models.PageModels;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class PlaylistService
    {
        private readonly MediaStudioContext postgres;
        private readonly PlaylistMinioService minioService;
        private readonly PlaylistBuilderService playlistBuilder;

        public PlaylistService(MediaStudioContext _postgres, PlaylistMinioService _minioService, PlaylistBuilderService _playlistBuilder)
        {
            postgres = _postgres;
            minioService = _minioService;
            playlistBuilder = _playlistBuilder;
        }

        public async Task<string> DeleteTrackAsync(long idTrackToPlaylist)
        {
            CheckTrackPlaylistExists(idTrackToPlaylist);

            var trackToPlaylist = postgres.TrackToPlaylist.FindAsync(idTrackToPlaylist);
            postgres.Remove(await trackToPlaylist);
            postgres.SaveChanges();

            return $"Трек из плейлиста успешно удален!";
        }

        public async Task<string> DeleteAsync(long idPlaylist)
        {
            // получаем плейлист с заданным id из БД
            var playlist = GetPlaylistByIdAsync(idPlaylist);

            var trackToPlaylist = postgres.TrackToPlaylist
                .Where(w => w.IdPlaylist == idPlaylist)
                .ToListAsync();

            postgres.Remove(await playlist);
            postgres.Remove(await trackToPlaylist);
            postgres.SaveChanges();

            return $"Плейлист успешно удален!";
        }

        public long AddTrack(long idTrack, long idPlaylist)
        {
            CheckTrackExists(idTrack);
            CheckPlaylistExists(idPlaylist);

            var trackToplaylist = new TrackToPlaylist
            {
                IdTrack = idTrack,
                IdPlaylist = idPlaylist,
            };

            postgres.Add(trackToplaylist);
            postgres.SaveChanges();

            return trackToplaylist.IdTrackToPlaylist;
        }

        public async Task<string> SetPublicStatusAsync(long IdPlaylist, bool isPublic)
        {
            var playlist = await GetPlaylistByIdAsync(IdPlaylist);
            playlist.IsPublic = isPublic;
            postgres.Update(playlist);
            await postgres.SaveChangesAsync();

            return "Статус плейлиста успешно изменен!";
        }

        public async Task<List<PagePlaylist>> GetOutsideSectionPlaylistsAsync(int idSection)
        {
            if (!postgres.Section.Any(section => section.IdSection == idSection))
                throw new MyNotFoundException($"Ошибка! В базе даных отсуствует секция с id {idSection}");

            var idSectionPlaylists = await postgres.SectionPlaylist
                .Where(sp => sp.IdSection == idSection)
                .Select(sp => sp.IdPlaylist)
                .ToListAsync();

            return await postgres.Playlist
                .AsNoTracking()
                .Where(playlist => playlist.IsPublic.Value && !idSectionPlaylists.Contains(playlist.IdPlaylist))
                .Select(playlist => new PagePlaylist
                {
                    IdPlaylist = playlist.IdPlaylist,
                    Name = playlist.Name,
                    LinkCover = minioService.MinioURL + playlist.PlaylistStorage
                        .Where(ps => ps.IdPlaylist == playlist.IdPlaylist && ps.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscovermedium)
                        .Select(ps => ps.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                }).ToListAsync();
        }
        public async Task<List<PagePlaylist>> GeneralList()
        {
            return await postgres.Playlist
                .AsNoTracking()
                .Where(playlist => playlist.IdAccount == null)
                .Select(playlist => new PagePlaylist
                {
                    IdPlaylist = playlist.IdPlaylist,
                    IsPublic = playlist.IsPublic.GetValueOrDefault(),
                    Name = playlist.Name,
                    ColourCode = playlist.IdColourNavigation.Code,
                    LinkCover = minioService.MinioURL + playlist.PlaylistStorage
                    .Where(playlistStorage => playlistStorage.IdPlaylist == playlist.IdPlaylist
                    && playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscovermedium)
                    .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                    .FirstOrDefault(),
                })
                .ToListAsync();
        }

        public async Task<PagePlaylist> GetAsync(long idPlaylist)
        {
            var audioBucket = BucketTypes.audio;
            CheckPlaylistExists(idPlaylist);
            var tracks = await playlistBuilder.GetListTracksAsync(idPlaylist, audioBucket);

            return await postgres.Playlist
               .AsNoTracking()
               .Where(playlist => playlist.IdPlaylist == idPlaylist)
               .Select(playlist => new PagePlaylist
               {
                   IdPlaylist = playlist.IdPlaylist,
                   Name = playlist.Name,
                   ColourCode = playlist.IdColourNavigation.Code,
                   LinkCover = minioService.MinioURL + playlist.PlaylistStorage
                   .Where(playlistStorage => playlistStorage.IdPlaylist == playlist.IdPlaylist
                        && playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscoverlarge)
                        .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                        .FirstOrDefault(),
                   Tracks = tracks,
               })
               .FirstOrDefaultAsync();
        }

        public async Task<PagePlaylist> GetAdminAsync(long idPlaylist)
        {
            CheckPlaylistExists(idPlaylist);
            var tracks = await playlistBuilder.GetListTracksAsync(idPlaylist, BucketTypes.audio);

            return await postgres.Playlist
               .AsNoTracking()
               .Where(playlist => playlist.IdPlaylist == idPlaylist)
               .Select(playlist => new PagePlaylist
               {
                   IdPlaylist = playlist.IdPlaylist,
                   Name = playlist.Name,
                   IsPublic = playlist.IsPublic.GetValueOrDefault(),
                   ColourCode = playlist.IdColourNavigation.Code,
                   LinkCover = minioService.MinioURL + playlist.PlaylistStorage
                   .Where(playlistStorage => playlistStorage.IdPlaylist == playlist.IdPlaylist
                        && playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscoverlarge)
                        .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                        .FirstOrDefault(),
                   Tracks = tracks,
               })
               .FirstOrDefaultAsync();
        }

        public string UpdateOrders(PlaylistOrderCollection playlistOrders)
        {
            foreach (var playlistOrder in playlistOrders.PlaylistOrderList)
            {
                UpdateOrder(playlistOrder);
            }

            return $"Порядок треков успешно изменен!";
        }

        public string UpdateOrder(InputPlaylistOrder trackOrder)
        {
            CheckTrackPlaylistExists(trackOrder.IdTrackToPlaylist);

            var trackToPlaylist = postgres.TrackToPlaylist.Find(trackOrder.IdTrackToPlaylist);
            trackToPlaylist.Order = trackOrder.IdNewOrder;
            postgres.Update(trackToPlaylist);
            postgres.SaveChanges();

            return $"Порядок трека успешно изменен!";
        }

        public async Task<string> UpdateCoverAsync(ImputCoverPlaylist playlistCover)
        {
            // получаем плейлист с заданным id из БД
            CheckPlaylistExists(playlistCover.IdPlaylist);

            if (playlistCover.Large != null)
                await minioService.UploadCoverAsync(playlistCover.IdPlaylist, playlistCover.Large, BucketTypes.playliscoverlarge);

            if (playlistCover.Medium != null)
                await minioService.UploadCoverAsync(playlistCover.IdPlaylist, playlistCover.Medium, BucketTypes.playliscovermedium);

            return $"Обложка Ппейлиста успешно изменена!";
        }

        public string Rename(long idPlaylist, string newName)
        {
            // проверяем, есть ли плейлист с таким id
            CheckPlaylistExists(idPlaylist);

            var playlist = postgres.Playlist.Find(idPlaylist);
            playlist.Name = newName;
            postgres.Update(playlist);
            postgres.SaveChanges();
            return $"Плейлист успешно переименован!";
        }

        public long New(Playlist playlist)
        {
            postgres.Playlist.Add(playlist);
            postgres.SaveChanges();
            return playlist.IdPlaylist;
        }

        private async Task<Playlist> GetPlaylistByIdAsync(long idPlaylist)
        {
            //проверяем, есть ли альбом с таким id
            CheckPlaylistExists(idPlaylist);

            ///получаем альбом с заданным id из БД
            return await postgres.Playlist.FindAsync(idPlaylist);
        }


        private void CheckTrackPlaylistExists(long id)
        {
            if (!postgres.TrackToPlaylist.Any(e => e.IdTrackToPlaylist == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден TrackToPlaylist с id {id}!");
        }

        public void CheckPlaylistExists(long idPlaylist)
        {
            if (!postgres.Playlist.Any(e => e.IdPlaylist == idPlaylist))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден плейлист с id {idPlaylist}!");
        }

        private void CheckTrackExists(long idTrack)
        {
            if (!postgres.Track.Any(e => e.IdTrack == idTrack))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден трек с id {idTrack}!");
        }
    }
}
