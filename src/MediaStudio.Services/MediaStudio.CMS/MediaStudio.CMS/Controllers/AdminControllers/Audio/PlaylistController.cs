namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Models.Input;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly PlaylistService _playlistService;

        public PlaylistController(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPut("New")]
        public long New(Playlist namePlaylist)
        {
            return _playlistService.New(namePlaylist);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpGet("GeneralList")]
        public async Task<List<PagePlaylist>> GeneralList()
        {
            return await _playlistService.GeneralList();
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpGet("OutsideSection")]
        public async Task<List<PagePlaylist>> Get(int idSection)
        {
            return await _playlistService.GetOutsideSectionPlaylistsAsync(idSection);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPost("UpdatePublicStatus")]
        public async Task<string> UpdatePublicStatus([FromBody] JObject jsonValue)
        {
            var IdPlaylist = jsonValue["idPlaylist"].ToObject<long>();
            var isPublic = jsonValue["isPublic"].ToObject<bool>();

            return await _playlistService.SetPublicStatusAsync(IdPlaylist, isPublic);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPost("Rename")]
        public string Rename([FromBody] JObject jsonValue)
        {
            var idPlaylist = jsonValue["idPlaylist"].ToObject<long>();
            var newName = jsonValue["newName"].ToString();

            return _playlistService.Rename(idPlaylist, newName);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPost("UpdateCover")]
        public async Task<string> UpdateCover([FromForm] ImputCoverPlaylist inputCover)
        {
            return await _playlistService.UpdateCoverAsync(inputCover);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpDelete("Delete")]
        public async Task<string> Delete(long idPlaylist)
        {
            return await _playlistService.DeleteAsync(idPlaylist);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpDelete("DeleteTrack")]
        public async Task<string> DeleteTrack(long idTrackToPlaylist)
        {
            return await _playlistService.DeleteTrackAsync(idTrackToPlaylist);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPost("UpdateOrder")]
        public string UpdateOrder(InputPlaylistOrder trackOrder)
        {
            return _playlistService.UpdateOrder(trackOrder);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPost("UpdateOrders")]
        public string UpdateOrders(PlaylistOrderCollection playlistOrders)
        {
            return _playlistService.UpdateOrders(playlistOrders);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpPut("AddTrack")]
        public long AddTrack([FromBody] JObject jsonValue)
        {
            var idTrack = jsonValue["idTrack"].ToObject<long>();
            var idPlaylist = jsonValue["idPlaylist"].ToObject<long>();

            return _playlistService.AddTrack(idTrack, idPlaylist);
        }

        [Authorize(Policy = Policy.FullPlaylist)]
        [HttpGet("{id}")]
        public async Task<PagePlaylist> Get(long id)
        {
            return await _playlistService.GetAdminAsync(id);
        }
    }
}