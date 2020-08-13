using MusicRater.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class AlbumRatingCreate
    {
        public int AlbumId { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Please provide a rating between 0.0 and 5.0")]
        public decimal AlbumIndividualRating { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public virtual Album Album { get; set; }
    }
}
