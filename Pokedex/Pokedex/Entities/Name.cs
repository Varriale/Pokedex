using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class Name
    {
        public string name { get; set; }
        public NamedAPIResource<Language> Language { get; set; }

    }
}
