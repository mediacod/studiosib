using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Track
    {
        public Track()
        {
            PerformerToTrack = new HashSet<PerformerToTrack>();
            TrackStorage = new HashSet<TrackStorage>();
            TrackToAlbum = new HashSet<TrackToAlbum>();
            TrackToPlaylist = new HashSet<TrackToPlaylist>();
            TrackToProperties = new HashSet<TrackToProperties>();
        }

        public long IdTrack { get; set; }
        public int IdTypeAudio { get; set; }
        public int? AlbumOrder { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime PublicationTime { get; set; }
        public bool IsDelete { get; set; }

        public virtual TypeAudio IdTypeAudioNavigation { get; set; }
        public virtual ICollection<PerformerToTrack> PerformerToTrack { get; set; }
        public virtual ICollection<TrackStorage> TrackStorage { get; set; }
        public virtual ICollection<TrackToAlbum> TrackToAlbum { get; set; }
        public virtual ICollection<TrackToPlaylist> TrackToPlaylist { get; set; }
        public virtual ICollection<TrackToProperties> TrackToProperties { get; set; }
    }
}
