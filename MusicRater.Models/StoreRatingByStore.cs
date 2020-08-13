using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class StoreRatingByStore
    {
        public int StoreRatingId { get; set; }
        public int StoreId { get; set; }
        public decimal StoreIndividualRating { get; set; }
        public Guid OwnerId { get; set; }
    }
}
