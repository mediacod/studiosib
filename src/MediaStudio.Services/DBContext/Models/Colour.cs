using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Colour
    {
        public Colour()
        {
            Playlist = new HashSet<Playlist>();
            Properties = new HashSet<Properties>();
        }

        public int IdColour { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<Playlist> Playlist { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
