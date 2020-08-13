using MusicRater.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class AlbumRatingCreate
    {
        public int AlbumId { get; set; }
        public decimal AlbumIndividualRating { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public virtual Album Album { get; set; }
    }
}
