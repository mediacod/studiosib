namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudio.Service.Services.UserHistory;
    using MediaStudioService.ApiModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserHistoryAlbumController : ControllerBase
    {
        private readonly UserHistoryAlbumService _historyAlbumService;

        public UserHistoryAlbumController(UserHistoryAlbumService historyAlbumService)
        {
            _historyAlbumService = historyAlbumService;
        }

        /// <summary>
        /// Получает список альбомов, которые прослушал пользователь.
        /// </summary>
        /// <returns>Список альбомов, которые прослушал пользователь.</returns>
        [HttpGet]
        public async Task<PageAlbum> Get()
        {
            return await _historyAlbumService.GetUserHistoryAlbums(User.Identity.Name);
        }

        /// <summary>
        /// Добавляет в историю прослушивания пользователем альбом.
        /// </summary>
        /// <param name="idAlbum">Идентификатор альбома для добавления в историю прослушивания.</param>
        /// <returns>Идентификатор истории пользователя альбома.</returns>
        [HttpPost]
        public long Post(int idAlbum)
        {
           return _historyAlbumService.AddUserHistoryAlbum(idAlbum, User.Identity.Name);
        }
    }
}
