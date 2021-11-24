using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Playlist
    {
        public Playlist()
        {
            PlaylistStorage = new HashSet<PlaylistStorage>();
            PlaylistToProperties = new HashSet<PlaylistToProperties>();
            SectionPlaylist = new HashSet<SectionPlaylist>();
            TrackToPlaylist = new HashSet<TrackToPlaylist>();
            UserFavouritesPlaylist = new HashSet<UserFavouritesPlaylist>();
            UserHistoryPlaylist = new HashSet<UserHistoryPlaylist>();
        }

        public long IdPlaylist { get; set; }
        public string Name { get; set; }
        public bool? IsPublic { get; set; }
        public int? IdAccount { get; set; }
        public int? IdColour { get; set; }

        public virtual Account IdAccountNavigation { get; set; }
        public virtual Colour IdColourNavigation { get; set; }
        public virtual ICollection<PlaylistStorage> PlaylistStorage { get; set; }
        public virtual ICollection<PlaylistToProperties> PlaylistToProperties { get; set; }
        public virtual ICollection<SectionPlaylist> SectionPlaylist { get; set; }
        public virtual ICollection<TrackToPlaylist> TrackToPlaylist { get; set; }
        public virtual ICollection<UserFavouritesPlaylist> UserFavouritesPlaylist { get; set; }
        public virtual ICollection<UserHistoryPlaylist> UserHistoryPlaylist { get; set; }
    }
}
