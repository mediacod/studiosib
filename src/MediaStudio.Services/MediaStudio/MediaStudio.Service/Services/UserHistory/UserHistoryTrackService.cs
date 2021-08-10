using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService;
using MediaStudioService.AccountServic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaStudio.Service.Services.UserHistory
{
    public class UserHistoryTrackService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly TrackService _trackService;
        private readonly AccountService _accountService;

        public UserHistoryTrackService(MediaStudioContext postgres, UserService userService, TrackService trackService, AccountService accountService)
        {
            _postgres = postgres;
            _userService = userService;
            _trackService = trackService;
            _accountService = accountService;
        }

        public IEnumerable<UserHistoryTrack> GetUserHistoryTracks(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return _postgres.UserHistoryTrack.AsNoTracking()
                .OrderByDescending(trHistory => trHistory.LastUse)
                .Take(50)
                .AsEnumerable();
        }

        public long AddUserHistoryTrack(long idTrack, string login)
        {
            _trackService.CheckTrackExists(idTrack);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyTrack = new UserHistoryTrack
            {
                IdTrack = idTrack,
                IdUser = idUser,
            };

            _postgres.UserHistoryTrack.Add(historyTrack);
            _postgres.SaveChanges();

            return historyTrack.IdUserHistoryTrack;
        }
    }
}

