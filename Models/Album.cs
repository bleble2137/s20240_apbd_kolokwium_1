using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos_apbd.Models
{
    public class Album
    {
        public int IdAlbum { get; set; }
        public string AlbumName { get; set; }
        public System.DateTime PublishDate { get; set; }
        public int IdMusicLabel { get; set; }
    }
}
