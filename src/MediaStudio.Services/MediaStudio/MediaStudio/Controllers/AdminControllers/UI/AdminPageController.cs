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
    public class AdminPageController : BasePageController
    {
        public AdminPageController(PageService pageService)
            : base(pageService)
        {
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpGet("List")]
        public async Task<List<PageModel>> GetList()
        {
            return await service.GetListAsync();
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddSection")]
        public int AddSection(PageSection page)
        {
            return service.AddSectionToPage(page);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPut("AddSections")]
        public string AddSections(List<PageSection> pageSections)
        {
            return service.AddSectionsToPage(pageSections);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPost("UpdateOrders")]
        public string UpdateOrders(List<InputSectionOrder> sectionOrders)
        {
            return service.UpdateOrders(sectionOrders);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpPost("UpdateOrder")]
        public string UpdateOrder(InputSectionOrder sectionOrder)
        {
            return service.UpdateOrder(sectionOrder);
        }

        [Authorize(Policy = Policy.FullPage)]
        [HttpDelete("DeleteSection")]
        public string DeleteSection(int IdPageSection)
        {
            return service.DeleteSection(IdPageSection);
        }
    }
}