namespace DBContext.Models
{
    public partial class PerformerToTrack
    {
        public long IdPerformerToTrack { get; set; }
        public long IdTrack { get; set; }
        public int IdPerformer { get; set; }

        public virtual Performer IdPerformerNavigation { get; set; }
        public virtual Track IdTrackNavigation { get; set; }
    }
}
