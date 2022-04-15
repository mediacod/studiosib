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

namespace MediaStudio.Service.Services.UserHistory
{
    public class UserHistoryPlaylistService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly PlaylistService _playlistService;
        private readonly AccountService _accountService;
        private readonly PlaylistMinioService _minioService;


        public UserHistoryPlaylistService(MediaStudioContext postgres, UserService userService, PlaylistService playlistService, AccountService accountService, PlaylistMinioService minioService)
        {
            _postgres = postgres;
            _userService = userService;
            _playlistService = playlistService;
            _accountService = accountService;
            _minioService = minioService;
        }

        public async Task<PagePlaylist> GetUserHistoryPlaylists(string login)
        {
            var idUser = _accountService.GetIdUserByLogin(login);
            return await _postgres.UserHistoryPlaylist.AsNoTracking()
                .Where(history => history.IdUser == idUser)
                .OrderByDescending(trHistory => trHistory.LastUse)
                .Distinct()
                .Take(50)
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

        public long AddUserHistoryPlaylist(long idPlaylist, string login)
        {
            _playlistService.CheckPlaylistExists(idPlaylist);
            var idUser = _accountService.GetIdUserByLogin(login);

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

