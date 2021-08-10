using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class SectionPlaylist
    {
        public int IdSectionPlaylist { get; set; }
        public int IdSection { get; set; }
        public long IdPlaylist { get; set; }

        public virtual Playlist IdPlaylistNavigation { get; set; }
        public virtual Section IdSectionNavigation { get; set; }
    }
}
