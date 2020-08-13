using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class ArtistRatingEdit
    {
        public int ArtistRatingId { get; set; }
        public int ArtistId { get; set; }
        public decimal ArtistIndividualRating { get; set; }

    }
}
