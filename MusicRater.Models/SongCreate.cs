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
        public string SongName { get; set; }

        public decimal Rating { get; set; }

        public int AlbumId { get; set; }

        public string AlbumName { get; set; }

        //Foreign Key

    }
}
