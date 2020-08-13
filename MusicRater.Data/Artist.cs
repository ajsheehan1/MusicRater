using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string ArtistName { get; set; }
        [Required]
        public decimal ArtistRating { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }
    }
}
