using System;

namespace MediaStudio.Service.Models.Input
{
    public class UserHistoryAlbumModel
    {
        public long IdUserHistoryAlbum { get; set; }
        public int IdAlbum { get; set; }
        public int IdUser { get; set; }
        public DateTime LastUse { get; set; }
    }
}
