namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudioService;
    using MediaStudioService.ApiModels;
    using MediaStudioService.Models.PageModels.ClientPage;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService albumService;

        public AlbumController(AlbumService albumService)
        {
            this.albumService = albumService;
        }

        /// <summary>
        /// Получить альбом.
        /// </summary>
        /// <param name="id">Идентификатор получаемого альбома.</param>
        /// <param name="isHighQuality">Качество получаемого альбома.</param>
        /// <returns>Альбом.</returns>
        [HttpGet("{id}")]
        public async Task<PageAlbum> GetAlbum(int id, bool isHighQuality = false)
        {
            return await albumService.GetAsync(id, isHighQuality);
        }

        /// <summary>
        /// Получить альбом по идентификатору трека.
        /// </summary>
        /// <param name="idTrack">Идентификатор трека.</param>
        /// <returns>Найденный альбом.</returns>
        [HttpGet("FindByTrack")]
        public async Task<SearchAlbumModel> FindByTrack(long idTrack)
        {
            return await albumService.GetAlbumByTrack(idTrack);
        }

        /// <summary>
        /// Получить коллекцию альбомов по типу аудио.
        /// </summary>
        /// <param name="idTypeAudio">Идентификатор типа аудио.</param>
        /// <returns>Найденные альбомы.</returns>
        [HttpGet("ByTypeAudio")]
        public async Task<SearchAlbumModel> ByTypeAudio(int idTypeAudio)
        {
            return await albumService.GetAlbumByTypeAudio(idTypeAudio);
        }
    }
}
