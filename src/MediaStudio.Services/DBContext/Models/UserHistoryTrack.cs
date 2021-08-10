using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class UserHistoryTrack
    {
        public long IdUserHistoryTrack { get; set; }
        public long IdTrack { get; set; }
        public int IdUser { get; set; }
        public DateTime LastUse { get; set; }

        public virtual Track IdTrackNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
