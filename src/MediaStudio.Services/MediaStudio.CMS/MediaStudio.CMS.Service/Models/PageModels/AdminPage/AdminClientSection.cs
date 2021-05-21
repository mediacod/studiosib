using System.Collections.Generic;

namespace MediaStudioService.Models.PageModels
{
    public class ClientSection
    {
        public string NameSection { get; set; }

        public IEnumerable<ClientSectionCell> Cells { get; set; }
    }
}
