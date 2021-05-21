namespace MediaStudio.Controllers.WebControllers
{
    using System.Threading.Tasks;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class PlaylistController : Controller
    {
        private readonly PlaylistService _playlistService;

        public PlaylistController(PlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet("{id}")]
        public async Task<PagePlaylist> Get(long id)
        {
            return await _playlistService.GetAsync(id);
        }
    }
}
