using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class UserFavouritesAlbum
    {
        public long IdUserFavouritesAlbum { get; set; }
        public int IdAlbum { get; set; }
        public int IdUser { get; set; }

        public virtual Album IdAlbumNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
