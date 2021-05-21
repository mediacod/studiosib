using System.Collections.Generic;

namespace MediaStudioService.ApiModels
{
    public class PageAlbum
    {
        public int IdAlbum { get; set; }
        public int IdTypeAudio { get; set; }
        public string Name { get; set; }
        public long? Duration { get; set; }
        public string LinkCover { get; set; }
        public bool HighQualityExist { get; set; }
        public bool IsChecked { get; set; }
        public List<PageTrack> Tracks { get; set; }
    }
}
