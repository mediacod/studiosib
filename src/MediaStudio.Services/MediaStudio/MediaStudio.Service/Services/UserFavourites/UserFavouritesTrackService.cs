using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService;
using MediaStudioService.AccountServic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaStudio.Service.Services.UserFavourites
{
    public class UserFavouritesTrackService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly TrackService _trackService;
        private readonly AccountService _accountService;

        public UserFavouritesTrackService(MediaStudioContext postgres, UserService userService, TrackService trackService, AccountService accountService)
        {
            _postgres = postgres;
            _userService = userService;
            _trackService = trackService;
            _accountService = accountService;
        }

        public IEnumerable<UserFavouritesTrack> GetUserFavouritesTracks(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return _postgres.UserFavouritesTrack.AsNoTracking()
                .OrderByDescending(trFavourites => trFavourites.IdUserFavouritesTrack)
                .Take(200)
                .AsEnumerable();
        }

        public long AddUserFavouritesTrack(long idTrack, string login)
        {
            _trackService.CheckTrackExists(idTrack);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyTrack = new UserFavouritesTrack
            {
                IdTrack = idTrack,
                IdUser = idUser,
            };

            _postgres.UserFavouritesTrack.Add(historyTrack);
            _postgres.SaveChanges();

            return historyTrack.IdUserFavouritesTrack;
        }
    }
}

