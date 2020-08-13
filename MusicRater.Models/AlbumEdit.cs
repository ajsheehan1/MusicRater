using MusicRater.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class AlbumEdit
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
        public decimal Rating { get; set; }
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }
        public int StoreId { get; set; }

    }
}
