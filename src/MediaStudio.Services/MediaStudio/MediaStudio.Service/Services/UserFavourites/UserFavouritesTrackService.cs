using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudio.Service.Services.Audit;
using MediaStudioService;
using MediaStudioService.AccountServic;
using MediaStudioService.ApiModels;
using MediaStudioService.Core.Enums;
using MediaStudioService.Service.ResourceService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudio.Service.Services.UserFavourites
{
    public class UserFavouritesTrackService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly TrackService _trackService;
        private readonly AccountService _accountService;
        private readonly TrackMinioService _minioService;


        public UserFavouritesTrackService(MediaStudioContext postgres, UserService userService, TrackService trackService, AccountService accountService, TrackMinioService minioService)
        {
            _postgres = postgres;
            _userService = userService;
            _trackService = trackService;
            _accountService = accountService;
            _minioService = minioService;
        }

        public async Task<List<PageTrack>> GetUserFavouritesTracks(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            var listPageTrack = await _postgres.UserHistoryTrack.AsNoTracking()
                .OrderByDescending(trHistory => trHistory.LastUse)
                .Take(100)
                .Select(userHistoryTrack => new PageTrack
                {
                    IdTrack = userHistoryTrack.IdTrackNavigation.IdTrack,
                    Name = userHistoryTrack.IdTrackNavigation.Name,
                    Duration = userHistoryTrack.IdTrackNavigation.Duration,
                    Link = userHistoryTrack.IdTrackNavigation.TrackStorage
                        .Where(tr => tr.IdTrack == userHistoryTrack.IdTrack
                         && tr.IdStorageNavigation.IdBucket == (short)BucketTypes.audio
                         && tr.IdStorageNavigation.ValidUntil > DateTime.Now)
                        .Select(trackStorage => trackStorage.IdStorageNavigation.TemporaryUrl)
                        .FirstOrDefault(),
                }).ToListAsync();

            foreach (var pageTrack in listPageTrack
                .Where(pageTrack => pageTrack.Link == null))
            {
                pageTrack.Link = await _minioService.BuildNewLinkAsync(pageTrack.IdTrack, BucketTypes.audio);
            }

            return listPageTrack;
        }

        public long AddUserFavouritesTrack(long idTrack, string login)
        {
            _trackService.CheckTrackExists(idTrack);
            var idUser = _accountService.GetIdUserByLogin(login);

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

