using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class PlaylistToProperties
    {
        public long IdPaylistToProperties { get; set; }
        public long IdPlaylist { get; set; }
        public int IdProp { get; set; }

        public virtual Playlist IdPlaylistNavigation { get; set; }
        public virtual Properties IdPropNavigation { get; set; }
    }
}
