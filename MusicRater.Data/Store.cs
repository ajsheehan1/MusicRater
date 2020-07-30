using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicRater.Data
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }
    }
}
