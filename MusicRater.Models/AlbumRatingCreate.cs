using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class AlbumRatingCreate
    {
        public int AlbumId { get; set; }
        public decimal AlbumIndividualRating { get; set; }
    }
}
