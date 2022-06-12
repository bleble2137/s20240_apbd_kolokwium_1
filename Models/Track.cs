using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolos_apbd.Models
{
    public class Track
    {
        public int IdTrack { get; set; }
        public string TrackName { get; set; }
        public float Duration { get; set; }
        public int IdMusicAlbum { get; set; }
    }
}
