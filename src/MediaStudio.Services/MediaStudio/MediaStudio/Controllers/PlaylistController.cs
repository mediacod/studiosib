namespace MediaStudio.Controllers.WebControllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly PlaylistService _playlistService;

        public PlaylistController(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        /// <summary>
        /// Получает плейлист по заданному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор плейлиста.</param>
        /// <returns>Плейлист</returns>
        [HttpGet("{id}")]
        public async Task<PagePlaylist> Get(long id)
        {
            return await _playlistService.GetAsync(id);
        }

        /// <summary>
        /// Создает пользовательский плейлист.
        /// </summary>
        /// <param name="playlist">Плейлист для создания</param>
        /// <returns>Идентификатор созданного плейлиста.</returns>
        [HttpPost]
        public long Post(Playlist playlist)
        {
            return _playlistService.CreateUserPlaylist(playlist, User.Identity.Name);
        }

        /// <summary>
        /// Обновляет пользовательский плейлист.
        /// </summary>
        /// <param name="playlist">Плейлист для обновления.</param>
        /// <returns>Идентификатор созданного плейлиста.</returns>
        [HttpPut("{id}")]
        public async Task<long> Put(Playlist playlist)
        {
            return await _playlistService.UpdateUserPlaylistAsync(playlist, User.Identity.Name);
        }

        /// <summary>
        /// Получает список плейлистов пользователя.
        /// </summary>
        /// <returns>Возаращает список плейлистов пользователя.</returns>
        [HttpGet]
        public async Task<List<PagePlaylist>> Get()
        {
            return await _playlistService.UserList(User.Identity.Name);
        }

        /// <summary>
        /// Удаляет трек из плейлиста.
        /// </summary>
        /// <param name="idTrackToPlaylist">Идентифкатор записи TrackToPlaylist. </param>
        /// <returns>Сообщение об успешном удаление трека из плейлиста.</returns>
        [HttpDelete("DeleteTrack")]
        public async Task<string> DeleteTrack(long idTrackToPlaylist)
        {
            return await _playlistService.DeleteUserTrackAsync(idTrackToPlaylist, User.Identity.Name);
        }

        /// <summary>
        /// Добавляет трек в плейлист.
        /// </summary>
        /// <param name="jsonValue">idTrack и idPlaylist в формате json параметра.</param>
        /// <returns>Идентификатор записи TrackToPlaylist</returns>
        [HttpPut("AddTrack")]
        public long AddTrack([FromBody] JObject jsonValue)
        {
            var idTrack = jsonValue["idTrack"].ToObject<long>();
            var idPlaylist = jsonValue["idPlaylist"].ToObject<long>();

            return _playlistService.AddUserTrack(idTrack, idPlaylist, User.Identity.Name);
        }
    }
}
