using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TypeAudio
    {
        public TypeAudio()
        {
            Album = new HashSet<Album>();
            GroupPropToTypeAudio = new HashSet<GroupPropToTypeAudio>();
            Track = new HashSet<Track>();
        }

        public int IdTypeAudio { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Album> Album { get; set; }
        public virtual ICollection<GroupPropToTypeAudio> GroupPropToTypeAudio { get; set; }
        public virtual ICollection<Track> Track { get; set; }
    }
}
