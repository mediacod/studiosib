namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using DBContext.Models;
    using MediaStudio.Service.Services.UserFavourites;
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
        public IEnumerable<UserFavouritesTrack> Get()
        {
            return _historyTrackService.GetUserFavouritesTracks(User.Identity.Name);
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
    }
}
