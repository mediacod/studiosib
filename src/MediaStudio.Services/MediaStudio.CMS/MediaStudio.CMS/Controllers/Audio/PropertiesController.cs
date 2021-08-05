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
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class PropertiesController : ControllerBase
    {
        private readonly PropertiesServices propServices;

        public PropertiesController(PropertiesServices propertiesServices)
        {
            this.propServices = propertiesServices;
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("New")]
        public string NewProp(Properties newProp)
        {
            return propServices.Add(newProp, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("List")]
        public IEnumerable<PagePropToGroup> GetPropList()
        {
            return propServices.GetAllPageProp();
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("ByGroup")]
        public PagePropToGroup GetByGroup(int idGroupProp)
        {
            return propServices.GetByGroup(idGroupProp);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("Rename")]
        public string UpdateName([FromBody] JObject jsonValue)
        {
            var idProp = jsonValue["idProp"].ToObject<int>();
            var newName = jsonValue["newName"].ToString();
            return propServices.Rename(idProp, newName, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpPost("UpdateColour")]
        public string UpdateColour([FromBody] JObject jsonValue)
        {
            var idProp = jsonValue["idProp"].ToObject<int>();
            var idNewColour = jsonValue["idNewColour"].ToObject<int>();
            return propServices.UpdateColour(idProp, idNewColour, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullProperties)]
        [HttpGet("AvailableColor")]
        public async Task<List<Colour>> AvailableColor()
        {
            return await propServices.AvailableColorAsync();
        }
    }
}
