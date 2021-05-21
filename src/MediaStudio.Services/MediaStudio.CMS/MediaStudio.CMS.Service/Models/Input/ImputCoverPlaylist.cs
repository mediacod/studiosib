using Microsoft.AspNetCore.Http;

namespace MediaStudioService.Models.Input
{
    public class ImputCoverPlaylist
    {
        public long IdPlaylist { get; set; }
        public IFormFile Medium { get; set; }
        public IFormFile Large { get; set; }
    }
}
