using DBContext.Models;
using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels.AdminPage
{
    public class AdminSection
    {
        public int IdSection { get; set; }

        public string NameSection { get; set; }

        public List<SectionAlbum> Albums { get; set; }

        public List<SectionPlaylist> Playlists { get; set; }
    }
}
