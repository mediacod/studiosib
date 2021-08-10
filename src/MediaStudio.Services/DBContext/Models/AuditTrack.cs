using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class AuditTrack
    {
        public int IdAuditTrack { get; set; }
        public string Action { get; set; }
        public long? IdTrack { get; set; }
        public string NameTrack { get; set; }
        public string ExecutorLogin { get; set; }
        public DateTime TimeOperation { get; set; }
        public bool IsSuccessful { get; set; }
        public string OldValue { get; set; }
    }
}
