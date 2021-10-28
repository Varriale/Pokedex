using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Entities
{
    public class EvolutionChain
    {
        public int Id { get; set; }
        public ChainLink Chain { get; set; }
    }
}
