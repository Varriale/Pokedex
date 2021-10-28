using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class PokemonSprites
    {
        public string Back_Default { get; set; }
        public string Back_Female { get; set; }
        public string Back_Shiny { get; set; }
        public string Back_Shiny_Female { get; set; }
        public string Front_Default { get; set; }
        public string Front_Female { get; set; }
        public string Front_Shiny { get; set; }
        public string Front_Shiny_Female { get; set; }
        public Dictionary<string,PokemonSprites> Other { get; set; }
    }
}
