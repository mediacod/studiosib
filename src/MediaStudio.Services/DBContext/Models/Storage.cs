using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public partial class Storage
    {
        public Storage()
        {
            AlbumStorage = new HashSet<AlbumStorage>();
            PlaylistStorage = new HashSet<PlaylistStorage>();
            TrackStorage = new HashSet<TrackStorage>();
        }

        public long IdStorage { get; set; }
        public int IdBucket { get; set; }
        public string ObjectName { get; set; }
        public string StaticUrl { get; set; }
        public string TemporaryUrl { get; set; }
        public DateTime? ValidUntil { get; set; }

        public virtual StorageBucket IdBucketNavigation { get; set; }
        public virtual ICollection<AlbumStorage> AlbumStorage { get; set; }
        public virtual ICollection<PlaylistStorage> PlaylistStorage { get; set; }
        public virtual ICollection<TrackStorage> TrackStorage { get; set; }
    }
}
