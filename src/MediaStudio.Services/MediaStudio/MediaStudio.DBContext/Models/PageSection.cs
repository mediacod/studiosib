namespace DBContext.Models
{
    public partial class PageSection
    {
        public int IdSectionPage { get; set; }
        public int IdPage { get; set; }
        public int IdSection { get; set; }
        public short OrderSection { get; set; }

        public virtual Page IdPageNavigation { get; set; }
        public virtual Section IdSectionNavigation { get; set; }
    }
}
