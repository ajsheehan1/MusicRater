using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongListItem
    {
        public int SongId { get; set; }

        public int AlbumId { get; set; }
        public string SongName { get; set; }

        public string AlbumName { get; set; }

        public string ArtistName { get; set; }

        public decimal Rating { get; set; }

        

        

        //Foreign Key??
    }
}
