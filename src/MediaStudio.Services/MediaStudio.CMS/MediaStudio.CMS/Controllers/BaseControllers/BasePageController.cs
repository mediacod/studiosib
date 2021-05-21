namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class BasePageController : ControllerBase
    {
        protected readonly PageService service;

        public BasePageController(PageService pageService)
        {
            service = pageService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ClientSection>> GetPageSections(int id)
        {
            return await service.GetPageSectionsAsync(id);
        }
    }
}