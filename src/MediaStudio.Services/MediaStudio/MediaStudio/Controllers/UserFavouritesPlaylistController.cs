namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudio.Service.Services.UserFavourites;
    using MediaStudioService.Models.PageModels;
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
        public async Task<PagePlaylist> Get()
        {
            return await _historyPlaylistService.GetUserFavouritesPlaylists(User.Identity.Name);
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
