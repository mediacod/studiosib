namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudio.Service.Services.UserHistory;
    using MediaStudioService.Models.PageModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserHistoryPlaylistController : ControllerBase
    {
        private readonly UserHistoryPlaylistService _historyPlaylistService;

        public UserHistoryPlaylistController(UserHistoryPlaylistService historyPlaylistService)
        {
            _historyPlaylistService = historyPlaylistService;
        }

        /// <summary>
        /// Получает список плейлистов, которые прослушал пользователь.
        /// </summary>
        /// <returns>Список плейлистов, которые прослушал пользователь.</returns>
        [HttpGet]
        public async Task<PagePlaylist> Get()
        {
            return await _historyPlaylistService.GetUserHistoryPlaylists(User.Identity.Name);
        }

        /// <summary>
        /// Добавляет в историю прослушивания пользователем плейлист.
        /// </summary>
        /// <param name="idPlaylist">Идентификатор плейлиста для добавления в историю прослушивания.</param>
        /// <returns>Идентификатор истории пользователя плейлиста.</returns>
        [HttpPost]
        public long Post(long idPlaylist)
        {
           return _historyPlaylistService.AddUserHistoryPlaylist(idPlaylist, User.Identity.Name);
        }
    }
}
