﻿namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudioService;
    using MediaStudioService.ApiModels;
    using MediaStudioService.Core;
    using MediaStudioService.Models.Input;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/album")]
    [ApiController]
    public class AdminAlbumController : BaseAlbumController
    {
        public AdminAlbumController(AlbumService albumService)
            : base(albumService)
        {
        }

        /// <summary>
        /// Возвращает альбом по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор альбома.</param>
        /// <param name="isHighQuality">Признак высокого качества.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PageAlbum), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<PageAlbum> GetAlbum(int id, bool isHighQuality = false)
        {
            return await albumService.GetAdminAsync(id, isHighQuality);
        }

        /// <summary>
        /// Удаляет альбом по идентификатору.
        /// </summary>
        /// <param name="idAlbum">Идентификатор альбома.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpDelete("Delete")]
        public async Task<string> Delete(int idAlbum)
        {
            return await albumService.Delete(idAlbum, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpPost("New")]
        public int NewAlbum([FromForm] InputAlbum inputAlbum)
        {
            return albumService.Create(inputAlbum, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpPut("UpdateCover")]
        public async Task<string> UpdateCover([FromForm] InputCoverAlbum coverAlbum)
        {
            return await albumService.UpdateAlbumCover(coverAlbum, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpPost("Rename")]
        public string Rename([FromBody] JObject jsonValue)
        {
            var idAlbum = jsonValue["idAlbum"].ToObject<int>();
            var newNameAlbum = jsonValue["newNameAlbum"].ToString();
            return albumService.Rename(idAlbum, newNameAlbum, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpPost("SetStatus")]
        public async Task<string> SetCheckedStatus([FromBody] JObject jsonValue)
        {
            var IdAlbum = jsonValue["idAlbum"].ToObject<int>();
            var isChecked = jsonValue["checked"].ToObject<bool>();
            return await albumService.SetCheckedStatusAsync(IdAlbum, isChecked, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpGet("OutsideSection")]
        public async Task<List<PageAlbum>> Get([FromQuery] InputOutsideSection inputOutsideSection)
        {
            return await albumService.GetOutsideSectionAlbumsAsync(inputOutsideSection);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpGet("List")]
        public async Task<List<PageAlbum>> GetAlbumList()
        {
            return await albumService.GetListAdminAsync(true);
        }

        [Authorize(Policy = Policy.FullAlbumControl)]
        [HttpGet("ListUnchecked")]
        public async Task<List<PageAlbum>> ListUnchecked()
        {
            return await albumService.GetListAdminAsync(false);
        }
    }
}
