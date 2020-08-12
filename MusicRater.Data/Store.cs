using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public decimal Rating { get; set; }
        public Guid OwnerId { get; set; }

        //[Required]
        //public int AlbumId { get; set; }
        //[ForeignKey(nameof(AlbumId))]
        public virtual ICollection<Album> Albums { get; set; }
        public Store()
        {
            this.Albums = new HashSet<Album>();
        }

    }
}


