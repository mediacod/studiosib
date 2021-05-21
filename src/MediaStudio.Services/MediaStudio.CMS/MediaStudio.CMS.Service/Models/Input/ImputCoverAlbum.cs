using Microsoft.AspNetCore.Http;

namespace MediaStudioService.Models.Input
{
    public class InputCoverAlbum
    {
        public int IdAlbum { get; set; }
        public IFormFile LargeCover { get; set; }
    }
}
