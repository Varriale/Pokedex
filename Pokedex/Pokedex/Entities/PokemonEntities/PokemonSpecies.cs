using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PokemonSpecies
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public bool Is_Baby { get; set; }
        public bool Is_Legendary { get; set; }
        public bool Is_Mythical { get; set; }
        public bool Has_Gender_differences { get; set; }
        public NamedAPIResource<PokemonSpecies> Evolves_From_Species { get; set; }
        public NamedAPIResource<EvolutionChain> Evolution_Chain { get; set; }
        public List<PokemonSpeciesVariety> Varieties { get; set; }
        public List<Name> Names { get; set; }

        public Pokemon defaultVariety { get; set; }

        public string enName
        {
            get
            {
                return Names.FirstOrDefault(n => n.Language.Name == "en")?.name??Name;
            }
        }
    }
}
