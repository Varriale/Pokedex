using Pokedex.Entities;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface IPokeAPIClient
    {
        Task<T> FetchResource<T>(NamedAPIResource<T> namedAPIResource);
    }
}
