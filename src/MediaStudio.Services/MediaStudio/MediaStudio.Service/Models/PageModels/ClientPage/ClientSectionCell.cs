namespace MediaStudioService.Models.PageModels
{
    public class ClientSectionCell
    {
        public long IdObject { get; set; }
        public short OrderSection { get; set; }
        public short IdTypeCell { get; set; }
        public string Name { get; set; }
        public string ColorCode { get; set; }
        public string LinkCover { get; set; }
        public int CountTrack { get; set; }
    }
}
