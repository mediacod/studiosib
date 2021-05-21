using MediaStudioService.ApiModels;
using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels
{
    public class PagePlaylist
    {
        public long IdPlaylist { get; set; }
        public string Name { get; set; }
        public string ColourCode { get; set; }
        public string LinkCover { get; set; }
        public bool IsPublic { get; set; }
        public List<PagePlaylistTrack> Tracks { get; set; }
    }
}
