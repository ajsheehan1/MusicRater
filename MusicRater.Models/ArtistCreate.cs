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
        [MinLength(1, ErrorMessage = "Please enter at least 1 character for the Artist Name")]

        public string ArtistName { get; set; }
        public decimal ArtistRating { get; set; }

    }
}
