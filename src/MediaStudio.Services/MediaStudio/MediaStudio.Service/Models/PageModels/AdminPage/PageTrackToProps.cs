using System.Collections.Generic;

namespace MediaStudioService.Models.Output
{
    public class PageTrackToProps
    {
        public long IdTrack { get; set; }

        public string Name { get; set; }

        public int? AlbumOrder { get; set; }

        public List<PageTrackProp> TrackProps { get; set; }
    }
}
