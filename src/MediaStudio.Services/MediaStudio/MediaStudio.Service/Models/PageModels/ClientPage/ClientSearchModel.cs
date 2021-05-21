using MediaStudioService.ApiModels;
using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels.ClientPage
{
    public class ClientSearchModel
    {
        public List<TrackSearchResult> Tracks { get; set; }

        public List<SearchAlbumModel> Albums { get; set; }

        public List<SearchPlaylistModel> Playlists { get; set; }

        public List<SearchPerformerModel> Perfomers { get; set; }
    }
}
