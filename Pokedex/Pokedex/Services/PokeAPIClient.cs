using Pokedex.Entities;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public class PokeAPIClient : IPokeAPIClient
    {
        static HttpClient client = new HttpClient();
        private readonly Uri _baseUri = new Uri("https://pokeapi.co/api/v2/");

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

        //Add the GetPokemon/SearchPokemon/GetPage ou coisa assim

    }
}
