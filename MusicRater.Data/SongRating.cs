using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class SongRating
    {
        [Key]
        public int SongRatingId { get; set; }
        [Required]
        public int SongId { get; set; }
        [ForeignKey(nameof(SongId))]
        public virtual Song Song { get; set; }
        [Required]
        public decimal SongIndividualRating { get; set; }
        [Required]
        public Guid OwnerId { get; set; }


    }
}
