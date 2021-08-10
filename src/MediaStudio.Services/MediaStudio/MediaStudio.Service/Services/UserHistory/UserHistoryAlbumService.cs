using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Models.Input;
using MediaStudio.Service.Services.Audit;
using MediaStudioService;
using MediaStudioService.AccountServic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaStudio.Service.Services.UserHistory
{
    public class UserHistoryAlbumService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly AlbumService _albumService;
        private readonly AccountService _accountService;

        public UserHistoryAlbumService(MediaStudioContext postgres, UserService userService, AlbumService albumService, AccountService accountService)
        {
            _postgres = postgres;
            _userService = userService;
            _albumService = albumService;
            _accountService = accountService;
        }

        public IEnumerable<UserHistoryAlbum> GetUserHistoryAlbums(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return _postgres.UserHistoryAlbum.AsNoTracking()
                .OrderByDescending(a => a.LastUse)
                .Take(20)
                .AsEnumerable();
        }


        public long AddUserHistoryAlbum(int idAlbum, string login)
        {
            _albumService.CheckAlbumExists(idAlbum);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyAlbum = new UserHistoryAlbum
            {
                IdAlbum = idAlbum,
                IdUser = idUser,
            };

            _postgres.UserHistoryAlbum.Add(historyAlbum);
            _postgres.SaveChanges();

            return historyAlbum.IdUserHistoryAlbum;
        }
    }
}

