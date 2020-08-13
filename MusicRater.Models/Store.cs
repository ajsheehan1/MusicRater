using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }
        public Guid OwnerId { get; set; }
    }
}
