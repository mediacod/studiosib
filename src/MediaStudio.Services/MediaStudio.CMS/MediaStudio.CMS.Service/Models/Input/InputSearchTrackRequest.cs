using System.Collections.Generic;

namespace MediaStudioService.Models.Input
{
    public class SearchTrackRequest
    {
        public List<long> IdProps { get; set; }
        public string NameTrack { get; set; }
        public int? IdTypeAudio { get; set; }
    }
}
