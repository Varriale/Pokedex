using Pokedex.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public interface IPokeAPIClient
    {
        Task<T> FetchResource<T>(NamedAPIResource<T> namedAPIResource);
        Task<PaginatedResource<T>> FetchPaginatedResource<T>(string url);
        Task<List<T>> FetchListResource<T>(List<NamedAPIResource<T>> namedAPIResourceList);
        Task<Pokemon> FetchPokemonFromPokemonApiModel(PokemonApiModel pkmModel);
        Task<List<Pokemon>> FetchListPokemonFromPokemonApiModel(List<PokemonApiModel> pkmModels);
        Entities.Type TypeFromName(string name);
    }
}
