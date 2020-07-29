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
        public string AlbumName { get; set; }
        public decimal Rating { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
