using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class SectionAlbum
    {
        public int IdSectionAlbum { get; set; }
        public int IdSection { get; set; }
        public int IdAlbum { get; set; }

        public virtual Album IdAlbumNavigation { get; set; }
        public virtual Section IdSectionNavigation { get; set; }
    }
}
