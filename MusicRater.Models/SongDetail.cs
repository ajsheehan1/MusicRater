using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongDetail
    {
        public int SongId { get; set; }

        public int AlbumId { get; set; }

        public string SongName { get; set; }
        public decimal Rating { get; set; }

        //public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }

        public string AlbumName { get; set; }
        
        public string ArtistName { get; set; }

    }
}
