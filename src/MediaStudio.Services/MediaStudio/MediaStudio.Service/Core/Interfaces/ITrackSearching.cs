using MediaStudioService.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaStudioService.Core.Interfaces
{
    public interface ITrackSearching<T>
    {
        public Task<List<TrackSearchResult>> SearchAdminAsync(T t);
        public List<TrackSearchResult> Search(T t);
    }
}