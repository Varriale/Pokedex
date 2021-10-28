using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class EvolutionDetail
    {
        public NamedAPIResource<EvolutionTrigger> Trigger { get; set; }
        public int Min_Level { get; set; }

    }
}
