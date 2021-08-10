using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TrackToPlaylist
    {
        public long IdTrackToPlaylist { get; set; }
        public long IdPlaylist { get; set; }
        public long IdTrack { get; set; }
        public int Order { get; set; }

        public virtual Playlist IdPlaylistNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
