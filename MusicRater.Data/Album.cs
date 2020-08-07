using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        
        [Required]
        public string AlbumName { get; set; }
        [Required]
        public decimal Rating  { get; set; }
        //public virtual Artist Artist { get; set; }//Should be foreign key
        public DateTimeOffset CreatedUtc { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [ForeignKey("ArtistId")]
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

    }
}
