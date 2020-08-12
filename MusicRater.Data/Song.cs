using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string SongName { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }

        [Required]
        public int AlbumId { get; set; }
        [ForeignKey(nameof(AlbumId))]
        public virtual Album Album { get; set;  }

        [Required]
        public Guid OwnerId { get; set; }
      
    }
}
