using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class ArtistDetail
    {
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }
        public decimal ArtistRating { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }

    }
}
