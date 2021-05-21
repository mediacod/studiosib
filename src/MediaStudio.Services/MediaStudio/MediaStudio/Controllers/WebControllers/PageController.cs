namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudioService.Models.PageModels.ClientPage;
    using MediaStudioService.Services;
    using MediaStudioService.Services.UI;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class PageController : BasePageController
    {
        private readonly ClientSearchService _clientSearchService;

        public PageController(ClientSearchService clientSearchService, PageService pageService)
             : base(pageService)
        {
            _clientSearchService = clientSearchService;
        }

        [HttpGet("Search")]
        public async Task<ClientSearchModel> SearchAll(string filter)
        {
            return await _clientSearchService.SearchAllAsync(filter);
        }
    }
}