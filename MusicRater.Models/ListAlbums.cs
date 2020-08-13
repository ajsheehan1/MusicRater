using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class ListAlbums
    {
        public int AlbumId { get; set; }

        public int ArtistId { get; set; }
        public string AlbumName { get; set; }

        public string ArtistName { get; set; }
        public decimal Rating { get; set; }
        
        //public DateTimeOffset CreatedUtc { get; set; } I removed this because I wasn't sure it was need. 
    }
}
