using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongEdit
    {
        public int SongId { get; set; }
        public string Title { get; set; }
        public decimal Rating { get; set; }

        public int AlbumId { get; set; }

    }
}
