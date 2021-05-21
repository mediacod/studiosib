using DBContext.Connect;
using DBContext.Models;
using MediaStudio.Classes.MyException;
using MediaStudioService.Models.Output;
using MediaStudioService.Services.audit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services
{
    public class TrackPropertiesService
    {
        private readonly MediaStudioContext postgres;
        private readonly AuditTrackService audit;

        public TrackPropertiesService(MediaStudioContext context, AuditTrackService auditTrackService)
        {
            postgres = context;
            audit = auditTrackService;
        }

        public async Task<PageTrackToProps> GetAsync(long idTrack)
        {
            CheckTrackExists(idTrack);

            var tracksProp = await postgres.TrackToProperties
                .AsNoTracking()
                .Where(t => t.IdTrack == idTrack)
                .Select(s => new PageTrackToProps
                {
                    IdTrack = s.IdTrack,
                    Name = s.IdTrackNavigation.Name,
                })
                .FirstOrDefaultAsync();

            tracksProp.TrackProps = await GetTrackPropsAsync(idTrack);
            return tracksProp;
        }

        public async Task<List<PageTrackToProps>> GetListAsync(int idAlbum)
        {
            CheckAlbumExists(idAlbum);

            var tracksProps = await postgres.TrackToAlbum
                .AsNoTracking()
                .Where(a => a.IdAlbum == idAlbum)
                .Select(s => new PageTrackToProps
                {
                    IdTrack = s.IdTrack,
                    Name = s.IdTrackNavigation.Name,
                    AlbumOrder = s.IdTrackNavigation.AlbumOrder,
                })
                .ToListAsync();

            foreach (var trackProps in tracksProps)
            {
                trackProps.TrackProps = await GetTrackPropsAsync(trackProps.IdTrack);
            }
            return tracksProps;
        }

        private async Task<List<PageTrackProp>> GetTrackPropsAsync(long idTrack)
        {
            return await postgres.TrackToProperties
                    .Where(t => t.IdTrack == idTrack)
                    .Select(s => new PageTrackProp
                    {
                        IdTrackToProperties = s.IdTrackToProperties,
                        IdGroupProp = s.IdPropNavigation.IdGroupProp,
                        IdProp = s.IdProp,
                    })
                    .ToListAsync();
        }

        public async Task<string> DeleteAsync(List<long> idTrackProps)
        {
            foreach (var idTrackProp in idTrackProps)
            {
                CheckTrackPropExists(idTrackProp);
                var trackProp = await postgres.TrackToProperties.FindAsync(idTrackProp);
                postgres.TrackToProperties.Remove(trackProp);
                await postgres.SaveChangesAsync();
            }
            return "Свойства треков успешно удалены";
        }

        public string Update(long idTrackProp, int idNewProp)
        {
            CheckTrackPropExists(idTrackProp);
            CheckPropertiesExists(idNewProp);

            var trackProp = postgres.TrackToProperties.FindAsync(idTrackProp);
            trackProp.Result.IdProp = idNewProp;
            postgres.SaveChanges();
            return "Своство трека успешно изменено";
        }

        public string New(List<TrackToProperties> newTrackToProps)
        {
            foreach (var newTrackToProp in newTrackToProps)
            {
                postgres.TrackToProperties.Add(newTrackToProp);
                postgres.SaveChanges();
            }
            return "Своства треков успешно добавлены";
        }

        private void CheckTrackPropExists(long id)
        {
            if (!postgres.TrackToProperties.Any(e => e.IdTrackToProperties == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найдено свойство трека под id {id}!");
        }

        private void CheckPropertiesExists(int id)
        {
            if (!postgres.Properties.Any(e => e.IdProp == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найдено свойство с id {id}!");
        }
        private void CheckTrackExists(long idTrack)
        {
            if (!postgres.Track.Any(e => e.IdTrack == idTrack))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден трек с id {idTrack}!");
        }

        private void CheckAlbumExists(int id)
        {
            if (!postgres.Album.Any(e => e.IdAlbum == id))
                throw new MyNotFoundException($"Ошибка! В базе данных не найден альбом с id {id}!");
        }
    }
}
