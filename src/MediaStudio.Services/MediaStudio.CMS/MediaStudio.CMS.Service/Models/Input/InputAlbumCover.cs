using Microsoft.AspNetCore.Http;

namespace MediaStudioService.Models.Input
{
    public class InputAlbumCover
    {
        public int IdAlbum { get; set; }
        public IFormFile Cover { get; set; }
    }
}
