using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class AuditAlbum
    {
        public long IdAuditAlbum { get; set; }
        public string Action { get; set; }
        public int? IdAlbum { get; set; }
        public string NameAlbum { get; set; }
        public string ExecutorLogin { get; set; }
        public DateTime TimeOperation { get; set; }
        public bool IsSuccessful { get; set; }
        public string OldValue { get; set; }
    }
}
