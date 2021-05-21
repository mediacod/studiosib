namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Models.PageModels.AdminPage;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SectionService service;

        public SectionController(SectionService sectionService)
        {
            service = sectionService;
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpGet("{idSection}")]
        public async Task<AdminClientSection> GetSection(int idSection)
        {
            return await service.GeSectionAsync(idSection);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpGet("List")]
        public async Task<List<AdminSection>> GetList()
        {
            return await service.GetListSection();
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("Add")]
        public int Add(Section newSection)
        {
            return service.AddSection(newSection);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPost("Rename")]
        public string Rename([FromBody] JObject jsonValue)
        {
            var idSection = jsonValue["idSection"].ToObject<int>();
            var newName = jsonValue["newName"].ToString();
            return service.Rename(idSection, newName);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddPlaylist")]
        public string AddPlaylist(SectionPlaylist sectionPlaylist)
        {
            return service.AddPlaylist(sectionPlaylist);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddPlaylists")]
        public string AddPlaylists(List<SectionPlaylist> sectionPlaylists)
        {
            return service.AddPlaylists(sectionPlaylists);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddAlbum")]
        public string AddAlbum(SectionAlbum sectionAlbum)
        {
            return service.AddAlbum(sectionAlbum);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddAlbums")]
        public string AddAlbums(List<SectionAlbum> sectionAlbums)
        {
            return service.AddAlbums(sectionAlbums);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpDelete("Delete")]
        public async Task<string> Delete(int idSection)
        {
            return await service.DeleteSectionAsync(idSection);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpDelete("DeleteAlbum")]
        public async Task<string> DeleteAlbum(int idSectionAlbum)
        {
            return await service.DeleteAlbumAsync(idSectionAlbum);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpDelete("DeletePlaylist")]
        public string DeletePlaylist(int idSectionPlaylist)
        {
            return service.DeletePlaylist(idSectionPlaylist);
        }
    }
}