using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class StoreRating
    {
        [Key]
        public int StoreRatingId { get; set; }
        [Required]
        public int StoreId { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }
        [Required]
        public decimal StoreIndividualRating { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
    }
}
