using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService.AccountServic;
using MediaStudioService.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaStudio.Service.Services.UserFavourites
{
    public class UserFavouritesPlaylistService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly PlaylistService _playlistService;
        private readonly AccountService _accountService;

        public UserFavouritesPlaylistService(MediaStudioContext postgres, UserService userService, PlaylistService playlistService, AccountService accountService)
        {
            _postgres = postgres;
            _userService = userService;
            _playlistService = playlistService;
            _accountService = accountService;
        }

        public IEnumerable<UserFavouritesPlaylist> GetUserFavouritesPlaylists(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return _postgres.UserFavouritesPlaylist.AsNoTracking()
                .OrderByDescending(trFavourites => trFavourites.IdUserFavouritesPlaylist)
                .Take(100)
                .AsEnumerable();
        }

        public long AddUserFavouritesPlaylist(long idPlaylist, string login)
        {
            _playlistService.CheckPlaylistExists(idPlaylist);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyPlaylist = new UserFavouritesPlaylist
            {
                IdPlaylist = idPlaylist,
                IdUser = idUser,
            };

            _postgres.UserFavouritesPlaylist.Add(historyPlaylist);
            _postgres.SaveChanges();

            return historyPlaylist.IdUserFavouritesPlaylist;
        }
    }
}

