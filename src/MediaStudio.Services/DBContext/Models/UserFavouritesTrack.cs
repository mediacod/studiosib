using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class UserFavouritesTrack
    {
        public long IdUserFavouritesTrack { get; set; }
        public long IdTrack { get; set; }
        public int IdUser { get; set; }

        public virtual Track IdTrackNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
