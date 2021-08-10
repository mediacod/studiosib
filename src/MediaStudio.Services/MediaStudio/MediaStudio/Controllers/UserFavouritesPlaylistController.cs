namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using DBContext.Models;
    using MediaStudio.Service.Services.UserFavourites;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserFavouritesPlaylistController : ControllerBase
    {
        private readonly UserFavouritesPlaylistService _historyPlaylistService;

        public UserFavouritesPlaylistController(UserFavouritesPlaylistService historyPlaylistService)
        {
            _historyPlaylistService = historyPlaylistService;
        }

        /// <summary>
        /// Получает список избранных плейлистов для текущего пользователя.
        /// </summary>
        /// <returns>Список избранных плейлистов текущего пользователя.</returns>
        [HttpGet]
        public IEnumerable<UserFavouritesPlaylist> Get()
        {
            return _historyPlaylistService.GetUserFavouritesPlaylists(User.Identity.Name);
        }

        /// <summary>
        /// Добавляет в избранное пользователя плейлист.
        /// </summary>
        /// <param name="idPlaylist">Идентификатор плейлиста для добавления в избранное пользователя.</param>
        /// <returns>Идентификатор избранного плейлиста пользователя .</returns>
        [HttpPost]
        public long Post(long idPlaylist)
        {
           return _historyPlaylistService.AddUserFavouritesPlaylist(idPlaylist, User.Identity.Name);
        }
    }
}
