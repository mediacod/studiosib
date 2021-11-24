namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudio.Service.Models.Input;
    using MediaStudio.Service.Services.UserFavourites;
    using MediaStudioService.ApiModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserFavouritesAlbumController : ControllerBase
    {
        private readonly UserFavouritesAlbumService _historyAlbumService;

        public UserFavouritesAlbumController(UserFavouritesAlbumService historyAlbumService)
        {
            _historyAlbumService = historyAlbumService;
        }

        /// <summary>
        /// Получает список избранных альбомов для текущего пользователя.
        /// </summary>
        /// <returns>Список избранных альбомов текущего пользователя.</returns>
        [HttpGet]
        public async Task<PageAlbum> Get()
        {
            return await _historyAlbumService.GetUserFavouritesAlbums(User.Identity.Name);
        }

        /// <summary>
        /// Добвляет в избранное пользователя альбом.
        /// </summary>
        /// <param name="idAlbum">Идентификатор альбома для добавления в избранное пользователя.</param>
        /// <returns>Идентификатор избранного альбома пользователем.</returns>
        [HttpPost]
        public long Post(int idAlbum)
        {
           return _historyAlbumService.AddUserFavouritesAlbum(idAlbum, User.Identity.Name);
        }
    }
}
