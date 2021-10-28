using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PaginatedResource<T>
    {
        public int Count { get; set; }
        public List<NamedAPIResource<T>> Results { get; set; }
        public string next { get; set; }
        public string previous { get; set; }

    }
}
