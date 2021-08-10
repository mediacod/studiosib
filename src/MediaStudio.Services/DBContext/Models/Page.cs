using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Page
    {
        public Page()
        {
            PageSection = new HashSet<PageSection>();
        }

        public int IdPage { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PageSection> PageSection { get; set; }
    }
}
