using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class ChainLink
    {
        public bool Is_Baby { get; set; }
        public NamedAPIResource<PokemonSpecies> Species { get; set; }
        //public List<EvolutionDetail> Evolution_Details { get; set; }
        public List<ChainLink> Evolves_To { get; set; }

    }
}
