namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Models.Output;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class TrackPropertiesController : ControllerBase
    {
        private readonly TrackPropertiesService trackPropServices;

        public TrackPropertiesController(TrackPropertiesService trackPropServices)
        {
            this.trackPropServices = trackPropServices;
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("Get")]
        public async Task<PageTrackToProps> Get(long idTrack)
        {
            return await trackPropServices.GetAsync(idTrack);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("List")]
        public async Task<List<PageTrackToProps>> GetList(int idAlbum)
        {
            return await trackPropServices.GetListAsync(idAlbum);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("New")]
        public string New(List<TrackToProperties> newTrackToProps)
        {
            return trackPropServices.New(newTrackToProps);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("Update")]
        public string Update([FromBody] JObject jsonValue)
        {
            var idTrackProp = jsonValue["idTrackProp"].ToObject<long>();
            var idNewProp = jsonValue["idNewProp"].ToObject<int>();
            return trackPropServices.Update(idTrackProp, idNewProp);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("Delete")]
        public async Task<string> Delete(List<long> idTrackProps)
        {
            return await trackPropServices.DeleteAsync(idTrackProps);
        }
    }
}
