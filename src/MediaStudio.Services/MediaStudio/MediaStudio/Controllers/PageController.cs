namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Models.PageModels.ClientPage;
    using MediaStudioService.Services;
    using MediaStudioService.Services.UI;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly ClientSearchService _clientSearchService;
        private readonly PageService _pageService;

        public PageController(ClientSearchService clientSearchService, PageService pageService)
        {
            _clientSearchService = clientSearchService;
            _pageService = pageService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ClientSection>> GetPageSections(int id)
        {
            return await _pageService.GetPageSectionsAsync(id);
        }

        [HttpGet("Search")]
        public async Task<ClientSearchModel> SearchAll(string filter)
        {
            return await _clientSearchService.SearchAllAsync(filter);
        }
    }
}