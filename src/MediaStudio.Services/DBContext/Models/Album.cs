using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Album
    {
        public Album()
        {
            AlbumStorage = new HashSet<AlbumStorage>();
            AlbumToProperties = new HashSet<AlbumToProperties>();
            SectionAlbum = new HashSet<SectionAlbum>();
            TrackToAlbum = new HashSet<TrackToAlbum>();
            UserFavouritesAlbum = new HashSet<UserFavouritesAlbum>();
            UserHistoryAlbum = new HashSet<UserHistoryAlbum>();
        }

        public int IdAlbum { get; set; }
        public int IdTypeAudio { get; set; }
        public string Name { get; set; }
        public long? Duration { get; set; }
        public bool IsChecked { get; set; }
        public DateTime PublicationTime { get; set; }
        public short? ReleaseYear { get; set; }
        public bool HighQualityExist { get; set; }
        public bool IsDelete { get; set; }

        public virtual TypeAudio IdTypeAudioNavigation { get; set; }
        public virtual ICollection<AlbumStorage> AlbumStorage { get; set; }
        public virtual ICollection<AlbumToProperties> AlbumToProperties { get; set; }
        public virtual ICollection<SectionAlbum> SectionAlbum { get; set; }
        public virtual ICollection<TrackToAlbum> TrackToAlbum { get; set; }
        public virtual ICollection<UserFavouritesAlbum> UserFavouritesAlbum { get; set; }
        public virtual ICollection<UserHistoryAlbum> UserHistoryAlbum { get; set; }
    }
}
