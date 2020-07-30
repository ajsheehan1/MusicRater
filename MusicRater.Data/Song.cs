using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }
      
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Rating { get; set; }

        // Will add foreign Key to Album/Artist later

        [Required]
        public Guid OwnerId { get; set; }
      
    }
}
