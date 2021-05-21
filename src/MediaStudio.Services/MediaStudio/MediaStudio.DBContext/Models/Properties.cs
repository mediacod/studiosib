using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Properties
    {
        public Properties()
        {
            AlbumToProperties = new HashSet<AlbumToProperties>();
            PlaylistToProperties = new HashSet<PlaylistToProperties>();
            TrackToProperties = new HashSet<TrackToProperties>();
        }

        public int IdProp { get; set; }
        public int IdGroupProp { get; set; }
        public string Name { get; set; }
        public int? IdColour { get; set; }

        public virtual Colour IdColourNavigation { get; set; }
        public virtual GroupProperties IdGroupPropNavigation { get; set; }
        public virtual ICollection<AlbumToProperties> AlbumToProperties { get; set; }
        public virtual ICollection<PlaylistToProperties> PlaylistToProperties { get; set; }
        public virtual ICollection<TrackToProperties> TrackToProperties { get; set; }
    }
}
