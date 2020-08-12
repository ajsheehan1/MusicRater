using MusicRater.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Models
{
    public class AlbumDetails
    {
        public int AlbumId { get; set; }
        public string AlbumName { get; set; }
      
        public decimal Rating { get; set; }
        public int ArtistId { get; set; }
      
        public Artist Artist { get; set; }
        public string ArtistName { get; set; }
        public decimal CulumativeRating { get; set; }
        public int NumberOfRatings { get; set; }
    }   
}
