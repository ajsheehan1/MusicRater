using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class SongListItem
    {
        public int SongId { get; set; }
        public string Title { get; set; }

        public decimal Rating { get; set; }

        //Foreign Key??
    }
}
