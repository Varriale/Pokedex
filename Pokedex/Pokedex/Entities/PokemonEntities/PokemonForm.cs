using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PokemonForm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Is_Default { get; set; }
        public bool Is_Battle_Only { get; set; }
        public bool Is_Mega { get; set; }
        public string Form_Name { get; set; }
        public NamedAPIResource<PokemonApiModel> Pokemon { get; set; }
        public List<NamedAPIResource<PokemonType>> Types { get; set; }
        public List<Name> Names { get; set; }
        public PokemonSprites Sprites { get; set; }

    }
}
