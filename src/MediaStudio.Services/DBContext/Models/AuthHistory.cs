using System;

namespace DBContext.Models
{
    public partial class AuthHistory
    {
        public long IdAuthHistory { get; set; }
        public string Action { get; set; }
        public string ExecutorLogin { get; set; }
        public string Ipv4 { get; set; }
        public string UserAgent { get; set; }
        public string NameDevice { get; set; }
        public DateTime TimeAction { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
