namespace MediaStudioService.ApiModels
{
    public class PageTrack
    {
        public long IdTrack { get; set; }
        public int? AlbumOrder { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Link { get; set; }
    }
}
