using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongCreate
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public decimal Rating { get; set; }

        public int AlbumId { get; set; }
        //Foreign Key??

    }
}
