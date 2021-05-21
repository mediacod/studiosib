using DBContext.Connect;
using DBContext.Models;
using MediaStudioService.ApiModels;
using MediaStudioService.Core.Interfaces;
using MediaStudioService.Models.Input;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStudioService.Services.AudioService
{
    public class TrackSearcService : ITrackSearching<SearchTrackRequest>
    {
        private readonly MediaStudioContext postgres;
        public TrackSearcService(MediaStudioContext _postgres)
        {
            postgres = _postgres;
        }

        public async Task<List<TrackSearchResult>> SearchAdminAsync(SearchTrackRequest searchTrackRequest)
        {
            CheckValidDataSearching(searchTrackRequest);
            var searchTask = await GetSearchTask(searchTrackRequest);
            return searchTask;
        }

        public List<TrackSearchResult> Search(SearchTrackRequest searchTrackRequest)
        {
            CheckValidDataSearching(searchTrackRequest);
            var searchTask = GetSearchTask(searchTrackRequest).Result;
            return searchTask;
        }

        private async Task<List<long>> GetFilterProperties(List<long> idProps)
        {
            return await postgres.TrackToProperties
                .AsNoTracking()
                .Where(w => idProps.Contains(w.IdProp))
                    .GroupBy(g => g.IdTrack)
                    .Where(w => w.Count() == idProps.Count())
                    .Select(s => s.Key)
                    .ToListAsync();
        }

        private async Task<List<long>> GetIdTracksByFilter(string nameTrack, int? idTypeAudio, List<long> idTracks)
        {
            var lowerNameTrack = $"%{nameTrack}%".ToLower();

            if (nameTrack != null && idTypeAudio == null)
            {
                return await postgres.Track
                    .Where(tr => EF.Functions.Like(tr.Name.ToLower(), lowerNameTrack))
                    .Take(200)
                    .Select(tr => tr.IdTrack)
                    .ToListAsync();
            }

            if (nameTrack == null && idTypeAudio != null)
            {
                return await postgres.Track
                    .Where(tr => idTracks.Contains(tr.IdTrack) && tr.IdTypeAudio == idTypeAudio)
                    .Take(200)
                    .Select(trackToProperties => trackToProperties.IdTrack)
                    .ToListAsync();
            }

            if (nameTrack != null && idTypeAudio != null)
            {
                return await postgres.Track
                    .Where(tr => EF.Functions.Like(tr.Name, lowerNameTrack)
                        && tr.IdTypeAudio == idTypeAudio)
                     .Take(200)
                    .Select(trackToProperties => trackToProperties.IdTrack)
                    .ToListAsync();
            }
            return null;
        }

        private async Task<List<TrackSearchResult>> GetSearchTask(SearchTrackRequest searchFilterRequest)
        {
            var idTracks = searchFilterRequest?.IdProps?.Count() > 0 
                ? await GetFilterProperties(searchFilterRequest.IdProps)
                : await postgres.TrackToProperties
                .AsNoTracking()
                 .Take(200)
                .Select(s => s.IdTrack)
                .ToListAsync();

            if(searchFilterRequest.NameTrack != null || searchFilterRequest.IdTypeAudio != null)
            {
                idTracks = await GetIdTracksByFilter(searchFilterRequest.NameTrack, searchFilterRequest.IdTypeAudio, idTracks);
            }

            return await postgres.Track
                .Where(track => idTracks.Contains(track.IdTrack))
                .AsNoTracking()
                .Take(200)
                .Select(track => new TrackSearchResult
                {
                    IdTrack = track.IdTrack,
                    Name = track.Name,
                })
                .ToListAsync();
        }

        private void CheckValidDataSearching(SearchTrackRequest searchTrackRequest)
        {
            if (searchTrackRequest.IdProps == null && searchTrackRequest.NameTrack == null && searchTrackRequest.IdTypeAudio == null)
                throw new InvalidOperationException($"Не выбраны параметры для поиска!");

            CheckTypeAudioExists(searchTrackRequest.IdTypeAudio);
            if (searchTrackRequest.IdProps == null) return;
            foreach (var idProp in searchTrackRequest.IdProps)
            {
                CheckPropertiesExists(idProp);
            }
        }

        private void CheckPropertiesExists(long id)
        {
            if (!postgres.Properties.Any(properties => properties.IdProp == id))
                throw new InvalidOperationException($"Ошибка! В базе данных не найдено свойство с id {id}!");
        }

        private void CheckTypeAudioExists(int? id)
        {
            if (!id.HasValue) return;
            if (!postgres.TypeAudio.Any(typeAudio => typeAudio.IdTypeAudio == id.Value))
                throw new InvalidOperationException($"Ошибка! В базе данных не найден тип аудио с id {id}!");
        }
    }
}
