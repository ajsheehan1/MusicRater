using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
   public  class AlbumRating
    {

        [Key]
        public int AlbumRatingId { get; set; }
        [Required]
        public int AlbumId { get; set; }

        [ForeignKey(nameof(AlbumId))]
        public virtual Album Album { get; set; }
     
        [Required]
        public decimal AlbumIndividualRating { get; set; }
     
        [Required]
        public Guid OwnerId { get; set; }

    }
}
