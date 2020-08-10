using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class ArtistRating
    {
        [Key]
        public int ArtistRatingId { get; set; }
        [Required]
        public int ArtistId { get; set; }
        [ForeignKey(nameof(ArtistId))]
        public virtual Artist Artist { get; set; }
        [Required]
        public decimal ArtistIndividualRating { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

    }
}
