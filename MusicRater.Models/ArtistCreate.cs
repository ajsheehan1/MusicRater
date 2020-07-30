using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class ArtistCreate
    {
        [Required]
        public string ArtistName { get; set; }
        public decimal ArtistRating { get; set; }

    }
}
