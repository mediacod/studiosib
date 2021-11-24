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

namespace MediaStudio.Service.Services.UserFavourites
{
    public class UserFavouritesAlbumService
    {
        private readonly MediaStudioContext _postgres;
        private readonly UserService _userService;
        private readonly AlbumService _albumService;
        private readonly AccountService _accountService;
        private readonly AlbumMinioService _minioService;

        public UserFavouritesAlbumService(MediaStudioContext postgres, UserService userService, AlbumService albumService, AccountService accountService, AlbumMinioService minioService)
        {
            _postgres = postgres;
            _userService = userService;
            _albumService = albumService;
            _accountService = accountService;
            _minioService = minioService;

        }

        public async Task<PageAlbum> GetUserFavouritesAlbums(string login)
        {
            var idAccount = _accountService.GetIdAccountByLogin(login);
            return await _postgres.UserFavouritesAlbum.AsNoTracking()
                .OrderByDescending(a => a.IdUserFavouritesAlbum)
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

        public long AddUserFavouritesAlbum(int idAlbum, string login)
        {
            _albumService.CheckAlbumExists(idAlbum);
            var idUser = _accountService.GetIdUserByLogin(login);

            if (idUser == default)
            {
                throw new MyNotFoundException($"В базе данных не найден пользователь для логина {login}!");
            }

            var historyAlbum = new UserFavouritesAlbum
            {
                IdAlbum = idAlbum,
                IdUser = idUser,
            };

            _postgres.UserFavouritesAlbum.Add(historyAlbum);
            _postgres.SaveChanges();

            return historyAlbum.IdUserFavouritesAlbum;
        }
    }
}

