namespace DBContext.Models
{
    public partial class TrackProperties
    {
        public long IdTrackProperties { get; set; }
        public long IdTrack { get; set; }
        public int IdProp { get; set; }

        public virtual Properties IdPropNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
