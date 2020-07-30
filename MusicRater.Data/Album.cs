using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public Guid OwnerId { get; set; }
        [Required]
        public string AlbumName { get; set; }
        [Required]
        public decimal Rating { get; set; }
        public virtual Artist Artist { get; set; }//Should be foreign key
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
