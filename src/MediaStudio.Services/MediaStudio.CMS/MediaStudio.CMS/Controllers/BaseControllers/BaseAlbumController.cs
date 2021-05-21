namespace MediaStudio.Controllers
{
    using MediaStudioService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    public class BaseAlbumController : ControllerBase
    {
        protected readonly AlbumService albumService;

        public BaseAlbumController(AlbumService albumService)
        {
            this.albumService = albumService;
        }
    }
}
