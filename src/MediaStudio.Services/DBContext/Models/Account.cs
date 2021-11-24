using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Account
    {
        public Account()
        {
            Playlist = new HashSet<Playlist>();
        }

        public int IdAccount { get; set; }
        public int IdTypeAccount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? TimeRegistration { get; set; }

        public virtual TypeAccount IdTypeAccountNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Playlist> Playlist { get; set; }
    }
}
