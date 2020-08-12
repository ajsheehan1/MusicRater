using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        
        public Guid OwnerId { get; set; }

        public decimal StoreRating { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }
    }
}


