using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PokemonType
    {
        public int Slot { get; set; }
        public NamedAPIResource<Pokedex.Entities.Type> Type { get; set; }
    }
}
