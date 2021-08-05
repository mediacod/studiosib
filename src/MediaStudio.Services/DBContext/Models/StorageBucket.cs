using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class StorageBucket
    {
        public StorageBucket()
        {
            Storage = new HashSet<Storage>();
        }

        public int IdStorageBucket { get; set; }
        public string NameBucket { get; set; }

        public virtual ICollection<Storage> Storage { get; set; }
    }
}
