namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Models.Input;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("admin/page")]
    [ApiController]
    public class AdminPageController : ControllerBase
    {
        private readonly PageService _pageService;

        public AdminPageController(PageService pageService)
        {
            _pageService = pageService;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<ClientSection>> GetPageSections(int id)
        {
            return await _pageService.GetPageSectionsAsync(id);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpGet("List")]
        public async Task<List<PageModel>> GetList()
        {
            return await _pageService.GetListAsync();
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddSection")]
        public int AddSection(PageSection page)
        {
            return _pageService.AddSectionToPage(page);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddSections")]
        public string AddSections(List<PageSection> pageSections)
        {
            return _pageService.AddSectionsToPage(pageSections);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPost("UpdateOrders")]
        public string UpdateOrders(List<InputSectionOrder> sectionOrders)
        {
            return _pageService.UpdateOrders(sectionOrders);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPost("UpdateOrder")]
        public string UpdateOrder(InputSectionOrder sectionOrder)
        {
            return _pageService.UpdateOrder(sectionOrder);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpDelete("DeleteSection")]
        public string DeleteSection(int IdPageSection)
        {
            return _pageService.DeleteSection(IdPageSection);
        }
    }
}