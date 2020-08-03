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
        public string Title { get; set; }

        [Required]
        public decimal Rating { get; set; }

        // Will add foreign Key to Album/Artist later
        //[Required]
        //public int CustomerId { get; set; }
        //[ForeignKey(nameof(CustomerId))]
        //public virtual Customer Customer { get; set; }

        [Required]
        public int AlbumId { get; set; }
        [ForeignKey(nameof(AlbumId))]
        public virtual Album Album { get; set;  }


        [Required]
        public Guid OwnerId { get; set; }
      
    }
}
