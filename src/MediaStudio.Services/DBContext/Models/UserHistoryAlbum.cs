using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class UserHistoryAlbum
    {
        public long IdUserHistoryTrack { get; set; }
        public int IdAlbum { get; set; }
        public int IdUser { get; set; }
        public DateTime LastUse { get; set; }

        public virtual Album IdAlbumNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
