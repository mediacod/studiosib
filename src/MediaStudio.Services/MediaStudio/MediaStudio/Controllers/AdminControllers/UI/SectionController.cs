namespace MediaStudio.Controllers
{
    using System.Threading.Tasks;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SectionService service;

        public SectionController(SectionService sectionService)
        {
            service = sectionService;
        }

        [HttpGet("{idSection}")]
        public async Task<AdminClientSection> GetSection(int idSection)
        {
            return await service.GeSectionAsync(idSection);
        }
    }
}