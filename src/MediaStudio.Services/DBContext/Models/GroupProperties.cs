using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class GroupProperties
    {
        public GroupProperties()
        {
            GroupPropToTypeAudio = new HashSet<GroupPropToTypeAudio>();
            Properties = new HashSet<Properties>();
        }

        public int IdGroupProp { get; set; }
        public string Name { get; set; }
        public bool? AllowMultiselect { get; set; }

        public virtual ICollection<GroupPropToTypeAudio> GroupPropToTypeAudio { get; set; }
        public virtual ICollection<Properties> Properties { get; set; }
    }
}
