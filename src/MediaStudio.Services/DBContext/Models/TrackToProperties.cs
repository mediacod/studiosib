using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TrackToProperties
    {
        public long IdTrackToProperties { get; set; }
        public long IdTrack { get; set; }
        public int IdProp { get; set; }

        public virtual Properties IdPropNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
