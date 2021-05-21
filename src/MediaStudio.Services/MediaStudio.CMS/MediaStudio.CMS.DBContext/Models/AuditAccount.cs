using System;

namespace DBContext.Models
{
    public partial class AuditAccount
    {
        public int IdAuditAccount { get; set; }
        public string Action { get; set; }
        public string AccountToEdit { get; set; }
        public int? IdAccount { get; set; }
        public string ExecutorLogin { get; set; }
        public DateTime TimeOperation { get; set; }
        public bool IsSuccessful { get; set; }
        public string OldValue { get; set; }
    }
}
