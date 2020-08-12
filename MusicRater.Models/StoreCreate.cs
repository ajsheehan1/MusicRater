using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class StoreCreate
    {
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string Address { get; set; }
      
        public decimal StoreRating { get; set; }

    }
}
