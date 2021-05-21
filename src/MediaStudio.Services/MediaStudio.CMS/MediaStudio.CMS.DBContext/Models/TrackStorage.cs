namespace DBContext.Models
{
    public partial class TrackStorage
    {
        public long IdTrackStorage { get; set; }
        public long IdTrack { get; set; }
        public long IdStorage { get; set; }

        public virtual Storage IdStorageNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
