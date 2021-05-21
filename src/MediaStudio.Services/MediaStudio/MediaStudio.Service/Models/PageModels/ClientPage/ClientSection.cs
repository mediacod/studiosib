using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels
{
    public class AdminClientSection
    {
        public int IdPage { get; set; }

        public string NameSection { get; set; }

        public IEnumerable<ClientSectionCell> Cells { get; set; }
    }
}
