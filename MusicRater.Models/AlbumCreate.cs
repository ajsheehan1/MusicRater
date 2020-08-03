using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
   public class AlbumCreate
    {
        [Required]
        [MinLength(1,ErrorMessage ="Please enter at least 1 character")]
        [MaxLength(20, ErrorMessage = "There are too many characters in this field")]
        public string AlbumName { get; set; }

        //[MaxLength(800)] 
        public decimal Rating { get; set; } //look up data annations for decimals

    }
}
