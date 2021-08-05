namespace MediaStudio.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DBContext.Models;
    using MediaStudioService.Core;
    using MediaStudioService.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;

    [Route("admin/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        readonly PerformerService performerService;

        public PerformerController(PerformerService performerServ)
        {
            this.performerService = performerServ;
        }

        [Authorize(Policy = Policy.FullPerformer)]
        [HttpGet("List")]
        public async Task<List<Performer>> GetAlbumList()
        {
            return await performerService.GetListAsync();
        }

        [Authorize(Policy = Policy.FullPerformer)]
        [HttpPost("New")]
        public string NewAlbum(Performer inputPerformer)
        {
            return performerService.Create(inputPerformer, User.Identity.Name);
        }

        [Authorize(Policy = Policy.FullPerformer)]
        [HttpPost("Rename")]
        public string Rename([FromBody] JObject jsonValue)
        {
            var idPerformer = jsonValue["idPerformer"].ToObject<int>();
            var newName = jsonValue["newName"].ToString();

            return performerService.Rename(idPerformer, newName, User.Identity.Name);
        }
    }
}