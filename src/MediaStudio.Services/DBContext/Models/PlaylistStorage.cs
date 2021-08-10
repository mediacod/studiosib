using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class PlaylistStorage
    {
        public long IdPlaylistStorage { get; set; }
        public long IdPlaylist { get; set; }
        public long IdStorage { get; set; }

        public virtual Playlist IdPlaylistNavigation { get; set; }
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
