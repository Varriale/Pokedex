using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Pokedex.Services
{
    public class PokeAPIClient : IPokeAPIClient
    {
        static HttpClient client = new HttpClient();
        public Dictionary<string, Entities.Type> Types = new Dictionary<string, Entities.Type>();
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/");

        public PokeAPIClient()
        {
            GetTypes();
        }
        private async void GetTypes()
        {
            PaginatedResource<Entities.Type> pagTypes = await FetchPaginatedResource<Entities.Type>(_baseUri + "type/?limit=100");
            foreach (NamedAPIResource<Entities.Type> namedResource in pagTypes.Results)
            {
                Types.Add(namedResource.Name, await FetchResource(namedResource));
            }
        }

        public async Task<T> FetchResource<T>(NamedAPIResource<T> namedAPIResource)
        {
            T resource = default(T);
            HttpResponseMessage response = await client.GetAsync(namedAPIResource.Url);
            if (response.IsSuccessStatusCode)
            {
                resource = await response.Content.ReadAsAsync<T>();
            }
            return resource;
        }
        public async Task<List<T>> FetchListResource<T>(List<NamedAPIResource<T>> namedAPIResourceList)
        {
            List<T> resultList = new List<T>();
            foreach (NamedAPIResource<T> namedResource in namedAPIResourceList)
            {
                resultList.Add(await FetchResource<T>(namedResource));
            }
            return resultList;
        }


        public async Task<PaginatedResource<T>> FetchPaginatedResource<T>(string url)
        {
            PaginatedResource<T> resource = default(PaginatedResource<T>);
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                resource = await response.Content.ReadAsAsync<PaginatedResource<T>>();
            }
            return resource;
        }

        public async Task<Pokemon> FetchPokemonFromPokemonApiModel(PokemonApiModel pkmModel)
        {
            Pokemon pkm = new Pokemon
            {
                Height = pkmModel.Height,
                Id = pkmModel.Id,
                Name = pkmModel.Name,
                Order = pkmModel.Order,
                Species = pkmModel.Species,
                Sprites = pkmModel.Sprites,
                Weight=pkmModel.Weight,
            };
            pkm.Types = pkmModel.Types.Select(t => TypeFromName(t.Type.Name)).ToList();
            pkm.Forms = await FetchListResource(pkmModel.Forms);
            return pkm;
        }

        public async Task<List<Pokemon>> FetchListPokemonFromPokemonApiModel(List<PokemonApiModel> pkmModels)
        {
            List<Pokemon> resultList = new List<Pokemon>();
            foreach (PokemonApiModel pkmModel in pkmModels)
            {
                resultList.Add(await FetchPokemonFromPokemonApiModel(pkmModel));
            }
            return resultList;
        }
        public Entities.Type TypeFromName(string name)
        {
            if (Types.ContainsKey(name))
            {
                return Types[name];
            }
            else
            {
                return Types["unknown"];
            }
            
        }

        //Add the GetPokemon/SearchPokemon/GetPage ou coisa assim

    }
}
