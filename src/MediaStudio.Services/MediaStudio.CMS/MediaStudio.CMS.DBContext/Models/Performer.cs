using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Performer
    {
        public Performer()
        {
            PerformerToTrack = new HashSet<PerformerToTrack>();
        }

        public int IdPerformer { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PerformerToTrack> PerformerToTrack { get; set; }
    }
}
