﻿namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using DBContext.Models;
    using MediaStudio.Service.Models.Input;
    using MediaStudio.Service.Services.UserHistory;
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
        public IEnumerable<UserHistoryAlbum> Get()
        {
            return _historyAlbumService.GetUserHistoryAlbums(User.Identity.Name);
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