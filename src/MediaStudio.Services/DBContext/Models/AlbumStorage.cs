namespace DBContext.Models
{
    public partial class AlbumStorage
    {
        public long IdAlbumStorage { get; set; }
        public int IdAbum { get; set; }
        public long? IdStorage { get; set; }

        public virtual Album IdAbumNavigation { get; set; }
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
