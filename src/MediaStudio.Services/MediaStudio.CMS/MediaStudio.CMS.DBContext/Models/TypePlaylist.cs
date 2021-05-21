using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TypePlaylist
    {
        public TypePlaylist()
        {
            Playlist = new HashSet<Playlist>();
        }

        public int IdTypePlaylist { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
