using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Section
    {
        public Section()
        {
            PageSection = new HashSet<PageSection>();
            SectionAlbum = new HashSet<SectionAlbum>();
            SectionPlaylist = new HashSet<SectionPlaylist>();
        }

        public int IdSection { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PageSection> PageSection { get; set; }
        public virtual ICollection<SectionAlbum> SectionAlbum { get; set; }
        public virtual ICollection<SectionPlaylist> SectionPlaylist { get; set; }
    }
}
