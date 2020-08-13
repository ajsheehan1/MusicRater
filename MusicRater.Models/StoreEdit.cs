using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class StoreEdit
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }

        public decimal StoreRating { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }

        //public decimal Rating { get; set; }
        //public Guid OwnerId { get; set; }
    }
}
