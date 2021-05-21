namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudioService;
    using MediaStudioService.ApiModels;
    using MediaStudioService.Core;
    using MediaStudioService.Core.Interfaces;
    using MediaStudioService.Models.Input;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private readonly TrackService trackService;
        private readonly ITrackSearching<SearchTrackRequest> searchService;

        public TrackController(TrackService _trackService, ITrackSearching<SearchTrackRequest> _searchService)
        {
            trackService = _trackService;
            searchService = _searchService;
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpGet("Find")]
        public async Task<List<TrackSearchResult>> Find([FromQuery] SearchTrackRequest searchTrackRequest)
        {
            return await searchService.SearchAdminAsync(searchTrackRequest);
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpPost("New")]
        public long NewTrack(InputTrack inputTrack)
        {
            return trackService.Add(inputTrack, User.Identity.Name);
        }

        [RequestSizeLimit(209715200)] // Лимит загружаемого файла, 200 мб (только на Kestrel)
        [Authorize(Policy = Policy.CreateTrack)]
        [HttpPut("Unload")]
        public async Task<long> UnloadTrack([FromForm] IFormFile audio, bool selectHQ = false)
        {
            return await trackService.UnloadAsync(audio, selectHQ, User.Identity.Name);
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpDelete("Delete")]
        public async Task<string> Delete(long idTrack)
        {
            return await trackService.DeleteAsync(idTrack, User.Identity.Name);
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpPost("Rename")]
        public string UpdateName([FromBody] JObject jsonValue)
        {
            var idTrack = jsonValue["idTrack"].ToObject<long>();
            var newNameTrack = jsonValue["newNameTrack"].ToString();
            return trackService.Rename(idTrack, newNameTrack, User.Identity.Name);
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpPost("UpdateOrder")]
        public string UpdateOrder(InputTrackToOrder trackToOrder)
        {
            return trackService.UpdateOrder(trackToOrder, User.Identity.Name);
        }

        [Authorize(Policy = Policy.CreateTrack)]
        [HttpPost("UpdateOrders")]
        public string UpdateOrders(TrackToOrderCollection orderClollection)
        {
            return trackService.UpdateOrders(orderClollection, User.Identity.Name);
        }
    }
}
