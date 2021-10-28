using Pokedex.Entities;
using Pokedex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Views
{
    public class ViewsDependancy
    {
        public readonly IPokeAPIClient pokeAPIClient;
        public DatabaseContext dbContext;
        public ViewsDependancy(IPokeAPIClient pokeAPIClient, DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            this.pokeAPIClient = pokeAPIClient;

        }
    }
}
