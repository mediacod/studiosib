using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService.AccountServic;
using MediaStudioService.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaStudio.Service.Services.UserHistory
{
    public class UserHistoryPlaylistService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly PlaylistService _playlistService;
        private readonly AccountService _accountService;

        public UserHistoryPlaylistService(MediaStudioContext postgres, UserService userService, PlaylistService playlistService, AccountService accountService)
        {
            _postgres = postgres;
            _userService = userService;
            _playlistService = playlistService;
            _accountService = accountService;
        }

        public IEnumerable<UserHistoryPlaylist> GetUserHistoryPlaylists(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return _postgres.UserHistoryPlaylist.AsNoTracking()
                .OrderByDescending(trHistory => trHistory.LastUse)
                .Take(20)
                .AsEnumerable();
        }

        public long AddUserHistoryPlaylist(long idPlaylist, string login)
        {
            _playlistService.CheckPlaylistExists(idPlaylist);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyPlaylist = new UserHistoryPlaylist
            {
                IdPlaylist = idPlaylist,
                IdUser = idUser,
            };

            _postgres.UserHistoryPlaylist.Add(historyPlaylist);
            _postgres.SaveChanges();

            return historyPlaylist.IdUserHistoryPlaylist;
        }
    }
}

