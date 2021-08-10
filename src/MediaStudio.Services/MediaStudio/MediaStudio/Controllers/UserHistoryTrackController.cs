namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudio.Service.Services.UserHistory;
    using MediaStudioService.ApiModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserHistoryTrackController : ControllerBase
    {
        private readonly UserHistoryTrackService _historyTrackService;

        public UserHistoryTrackController(UserHistoryTrackService historyTrackService)
        {
            _historyTrackService = historyTrackService;
        }

        /// <summary>
        /// Получает список треков, которые прослушал пользователь.
        /// </summary>
        /// <returns>Список треков, которые прослушал пользователь.</returns>
        [HttpGet]
        public async Task<List<PageTrack>> Get()
        {
            return await _historyTrackService.GetUserHistoryTracks(User.Identity.Name);
        }

        /// <summary>
        /// Добавляет в историю прослушивания пользователем трек.
        /// </summary>
        /// <param name="idTrack">Идентификатор трека для добавления в историю прослушивания.</param>
        /// <returns>Идентификатор истории пользователя трека.</returns>
        [HttpPost]
        public long Post(int idTrack)
        {
           return _historyTrackService.AddUserHistoryTrack(idTrack, User.Identity.Name);
        }
    }
}
