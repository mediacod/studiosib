using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class UserHistoryPlaylist
    {
        public long IdUserHistoryTrack { get; set; }
        public long IdPlaylist { get; set; }
        public int IdUser { get; set; }
        public DateTime LastUse { get; set; }

        public virtual Playlist IdPlaylistNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
