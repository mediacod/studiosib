using MediaStudioService.Models.PageModels.AdminPage;
using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels
{
    public class PageModel
    {
        public int IdPage { get; set; }
        public string Name { get; set; }

        public List<AdminPageSection> PageSections { get; set; }
    }
}
