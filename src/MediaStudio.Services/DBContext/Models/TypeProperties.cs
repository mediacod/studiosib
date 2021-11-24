using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class TypeProperties
    {
        public TypeProperties()
        {
            Properties = new HashSet<Properties>();
        }

        public int IdTypeProp { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Properties> Properties { get; set; }
    }
}
