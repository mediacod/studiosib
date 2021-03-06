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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudio.Service.Services.UserHistory
{
    public class UserHistoryAlbumService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly AlbumService _albumService;
        private readonly AccountService _accountService;
        private readonly AlbumMinioService _minioService;


        public UserHistoryAlbumService(MediaStudioContext postgres, UserService userService, AlbumService albumService, AccountService accountService, AlbumMinioService minioService)
        {
            _postgres = postgres;
            _userService = userService;
            _albumService = albumService;
            _accountService = accountService;
            _minioService = minioService;
        }

        public async Task<PageAlbum> GetUserHistoryAlbums(string login)
        {
            var idUser = _accountService.GetIdUserByLogin(login);
            return await _postgres.UserHistoryAlbum.AsNoTracking()
                .Where(history => history.IdUser == idUser)
                .OrderByDescending(a => a.LastUse)
                .Distinct()
                .Take(50)
                .Select(album => new PageAlbum
                {
                    IdAlbum = album.IdAlbum,
                    Name = album.IdAlbumNavigation.Name,
                    Duration = album.IdAlbumNavigation.Duration,
                    IdTypeAudio = album.IdAlbumNavigation.IdTypeAudio,
                    LinkCover = _minioService.MinioURL + album.IdAlbumNavigation.AlbumStorage
                    .Where(albumCloud => albumCloud.IdAbum == album.IdAlbum
                    && albumCloud.IdStorageNavigation.IdBucket == (int)BucketTypes.albumcovermedium)
                    .Select(s => s.IdStorageNavigation.StaticUrl).FirstOrDefault(),
                })
                .FirstOrDefaultAsync();
        }

        public long AddUserHistoryAlbum(int idAlbum, string login)
        {
            _albumService.CheckAlbumExists(idAlbum);
            var idUser = _accountService.GetIdUserByLogin(login);

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

