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
    public class StoreRatingCreate
    {
        public int StoreId { get; set; }
        public decimal StoreIndividualRating { get; set; }

        [ForeignKey(nameof(StoreId))]
        public virtual Store Store { get; set; }

    }
}
