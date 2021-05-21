using DBContext.Models;
using MediaStudioService.Models.Input;

namespace MediaStudioService.ModelBulder
{
    public class TrackBuilderService
    {
        public Track BuldPostgresTrack(InputTrack inputTrack)
        {
            var track = new Track()
            {
                Name = inputTrack.Name,
                Duration = inputTrack.Duration,
                AlbumOrder = inputTrack.AlbumOrder,
                IdTypeAudio = inputTrack.IdTypeAudio,
                PublicationTime = inputTrack.PublicationTime,
            };
            return track;
        }
    }
}
