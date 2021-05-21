using System;

namespace MediaStudioService.Models.Input
{
    public class InputAlbum
    {
        public string Name { get; set; }
        public short IdTypeAudio { get; set; }
        public short ReleaseYear { get; set; }
        public DateTime PublicationTime { get; set; }
        public bool HighQualityExist { get; set; }
    }
}
