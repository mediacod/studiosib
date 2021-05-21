namespace MediaStudioService.ApiModels
{
    public class PagePlaylistTrack
    {
        public long IdTrackToPlaylist { get; set; }
        public long IdTrack { get; set; }
        public int? Order { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Link { get; set; }
    }
}
