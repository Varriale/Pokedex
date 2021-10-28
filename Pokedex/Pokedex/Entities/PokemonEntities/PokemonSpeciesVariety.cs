using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PokemonSpeciesVariety
    {
        public bool Is_Default { get; set; }
        public NamedAPIResource<PokemonApiModel> Pokemon { get; set; }
    }
}
