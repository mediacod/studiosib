namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Models.PageModels;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("admin/[controller]")]
    [ApiController]
    public class GroupPropertiesController : ControllerBase
    {
        private readonly PropertiesServices propServices;

        public GroupPropertiesController(PropertiesServices propertiesServices)
        {
            this.propServices = propertiesServices;
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("Available")]
        public async Task<List<PageAvalibleGroupProp>> GetAvailableGroups(int idTypeAudio)
        {
            return await propServices.GetAvailableGroups(idTypeAudio);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("List")]
        public async Task<List<GroupProperties>> GetGroupList()
        {
            return await propServices.GetGroupList();
        }
    }
}
