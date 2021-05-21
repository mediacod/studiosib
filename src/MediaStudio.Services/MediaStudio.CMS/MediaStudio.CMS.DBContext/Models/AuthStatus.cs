using System;

namespace DBContext.Models
{
    public partial class AuthStatus
    {
        public int IdAuthStatus { get; set; }
        public string Login { get; set; }
        public string Jwt { get; set; }
        public string Refresh { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }
        public bool? IsValid { get; set; }
        public string Ipv4 { get; set; }
        public string UserAgent { get; set; }
        public string NameDevice { get; set; }
    }
}
