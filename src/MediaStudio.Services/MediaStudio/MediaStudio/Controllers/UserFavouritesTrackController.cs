namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudio.Service.Services.UserFavourites;
    using MediaStudioService.ApiModels;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class UserFavouritesTrackController : ControllerBase
    {
        private readonly UserFavouritesTrackService _historyTrackService;

        public UserFavouritesTrackController(UserFavouritesTrackService historyTrackService)
        {
            _historyTrackService = historyTrackService;
        }

        /// <summary>
        /// Получает список избранных треков для текущего пользователя.
        /// </summary>
        /// <returns>Список избранных треков текущего пользователя.</returns>
        [HttpGet]
        public async Task<List<PageTrack>> Get()
        {
            return await _historyTrackService.GetUserFavouritesTracks(User.Identity.Name);
        }

        /// <summary>
        /// Добавляет в избранное пользователя трек.
        /// </summary>
        /// <param name="idTrack">Идентификатор трека для добавления в избранное пользователя.</param>
        /// <returns>Идентификатор избраного трека пользователя.</returns>
        [HttpPost]
        public long Post(int idTrack)
        {
           return _historyTrackService.AddUserFavouritesTrack(idTrack, User.Identity.Name);
        }

        /// <summary>
        /// Удаляет трек пользователя из избранного.
        /// </summary>
        /// <param name="idTrack">Идентификатор трека для удаления из избранного пользователя.</param>
        /// <returns>Сообщение об успешном удаление трека из избранного пользователя.</returns>
        [HttpDelete]
        public string Delete(int idTrack)
        {
            return _historyTrackService.DeleteUserFavouritesTrack(idTrack, User.Identity.Name);
        }
    }
}
