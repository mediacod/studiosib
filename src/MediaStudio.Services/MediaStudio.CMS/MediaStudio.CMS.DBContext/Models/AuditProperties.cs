using System;

namespace DBContext.Models
{
    public partial class AuditProperties
    {
        public long IdAuditProp { get; set; }
        public string Action { get; set; }
        public int? IdProp { get; set; }
        public string NameProp { get; set; }
        public string ExecutorLogin { get; set; }
        public DateTime TimeOperation { get; set; }
        public bool IsSuccessful { get; set; }
        public string OldValue { get; set; }
    }
}
