using System;

namespace DBContext.Models
{
    public partial class AuditPerformer
    {
        public long IdAuditPerformer { get; set; }
        public string Action { get; set; }
        public int? IdPerformer { get; set; }
        public string NamePerformer { get; set; }
        public string ExecutorLogin { get; set; }
        public DateTime TimeOperation { get; set; }
        public bool IsSuccessful { get; set; }
        public string OldValue { get; set; }
    }
}
