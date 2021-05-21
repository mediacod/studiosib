namespace DBContext.Models
{
    public partial class TrackToAlbum
    {
        public long IdTrackToAlbum { get; set; }
        public int IdAlbum { get; set; }
        public long IdTrack { get; set; }

        public virtual Album IdAlbumNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
