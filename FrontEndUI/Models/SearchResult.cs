using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndUI.Models
{
    class SearchResult<T>
    {
        public int Count { get; set; }
        public List<T> Results { get; set; } = new List<T>();
    }
}
