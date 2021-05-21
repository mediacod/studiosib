using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels
{
    public class PagePropToGroup
    {
        public int IdGroupProp { get; set; }
        public string NameGroup { get; set; }
        public List<PageProperties> Properties { get; set; }
    }
}
