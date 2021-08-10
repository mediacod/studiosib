using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TypeAccount
    {
        public TypeAccount()
        {
            Account = new HashSet<Account>();
        }

        public int IdTypeAccount { get; set; }
        public string NameType { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
