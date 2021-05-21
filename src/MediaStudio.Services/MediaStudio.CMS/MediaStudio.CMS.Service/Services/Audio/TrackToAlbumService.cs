using DBContext.Connect;
using DBContext.Models;

namespace MediaStudioService.Services
{
    public class TrackAlbumService
    {
        private readonly MediaStudioContext postgres;
        public TrackAlbumService(MediaStudioContext context)
        {
            postgres = context;
        }
        public void AddRecord(int idAlbum, long idTrack)
        {
            var trackToAlbum = new TrackToAlbum()
            {
                IdTrack = idTrack,
                IdAlbum = idAlbum,
            };

            postgres.TrackToAlbum.Add(trackToAlbum);
            postgres.SaveChanges();
        }
    }
}
