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
    public class SongRatingCreate
    {
        public int SongId { get; set; }

        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }

        [Range(0.0, 5.0, ErrorMessage = "Please provide a rating between 0.0 and 5.0")]
        public decimal SongIndividualRating { get; set; }

      
    }
}
