using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService.AccountServic;
using MediaStudioService.Core.Enums;
using MediaStudioService.Models.PageModels;
using MediaStudioService.Service.ResourceService;
using MediaStudioService.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudio.Service.Services.UserFavourites
{
    public class UserFavouritesPlaylistService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly PlaylistService _playlistService;
        private readonly AccountService _accountService;
        private readonly PlaylistMinioService _minioService;

        public UserFavouritesPlaylistService(MediaStudioContext postgres, UserService userService, PlaylistService playlistService, AccountService accountService,  PlaylistMinioService minioService)
        {
            _postgres = postgres;
            _userService = userService;
            _playlistService = playlistService;
            _accountService = accountService;
            _minioService = minioService;

        }

        public async Task<PagePlaylist> GetUserFavouritesPlaylists(string login)
        {
            var idUser = _accountService.GetIdUserByLogin(login);
            return await _postgres.UserFavouritesPlaylist.AsNoTracking()
                .Where(favourites => favourites.IdUser == idUser)
                .OrderByDescending(trFavourites => trFavourites.IdUserFavouritesPlaylist)
                .Take(100)
                .Select(playlist => new PagePlaylist
                {
                    IdPlaylist = playlist.IdPlaylist,
                    Name = playlist.IdPlaylistNavigation.Name,
                    ColourCode = playlist.IdPlaylistNavigation.IdColourNavigation.Code,
                    LinkCover = _minioService.MinioURL + playlist.IdPlaylistNavigation.PlaylistStorage
                       .Where(playlistStorage => playlistStorage.IdPlaylist == playlist.IdPlaylist
                            && playlistStorage.IdStorageNavigation.IdBucket == (int)BucketTypes.playliscovermedium)
                            .Select(playlistStorage => playlistStorage.IdStorageNavigation.StaticUrl)
                            .FirstOrDefault(),
                     Tracks = null,
                 }).FirstOrDefaultAsync();
        }

        public long AddUserFavouritesPlaylist(long idPlaylist, string login)
        {
            _playlistService.CheckPlaylistExists(idPlaylist);
            var idUser = _accountService.GetIdUserByLogin(login);


            var historyPlaylist = new UserFavouritesPlaylist
            {
                IdPlaylist = idPlaylist,
                IdUser = idUser,
            };

            _postgres.UserFavouritesPlaylist.Add(historyPlaylist);
            _postgres.SaveChanges();

            return historyPlaylist.IdUserFavouritesPlaylist;
        }

        public string DeleteUserFavouritesPlaylist(int idPlaylist, string login)
        {
            _playlistService.CheckPlaylistExists(idPlaylist);
            var idUser = _accountService.GetIdUserByLogin(login);

            var userFavouritesPlaylist = _postgres.UserFavouritesPlaylist.AsNoTracking()
                            .Where(favourites => favourites.IdUser == idUser && favourites.IdPlaylist == idPlaylist)
                            .FirstOrDefault();

            _postgres.Remove(userFavouritesPlaylist);
            _postgres.SaveChanges();

            return $"Плейлист с id {idPlaylist} успешно удален из избранного пользователя!";
        }
    }
}

