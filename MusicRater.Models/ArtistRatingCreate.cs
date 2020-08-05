using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class ArtistRatingCreate
    {
        public int ArtistId { get; set; }
        public decimal ArtistIndividualRating { get; set; }

    }
}
