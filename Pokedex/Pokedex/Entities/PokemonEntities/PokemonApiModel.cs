using System.Collections.Generic;

namespace Pokedex.Entities
{
    public class PokemonApiModel
    {
        public string Name { get; set; }
        public List<NamedAPIResource<PokemonForm>> Forms { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public int Weight { get; set; }
        public PokemonSprites Sprites { get; set; }
        public NamedAPIResource<PokemonSpecies> Species { get; set; }
        public List<PokemonType> Types { get; set; }
    }
}
