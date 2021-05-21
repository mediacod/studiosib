using System;

namespace MediaStudioService.Models.Input
{
    public class InputTrack
    {
        public long IdTrack { get; set; }
        public string Name { get; set; }
        public int IdTypeAudio { get; set; }
        public int? AlbumOrder { get; set; }
        public int Duration { get; set; }
        public int IdAlbum { get; set; }
        public long IdStorage { get; set; }
        public long? IdStorageHQ { get; set; }
        public DateTime PublicationTime { get; set; }
    }
}
