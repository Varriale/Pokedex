using Pokedex.Entities;
using Pokedex.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para NationalPokedex.xam
    /// </summary>
    public partial class NationalPokedex : Page
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        public ObservableCollection<PaginatedResource<PokemonSpecies>> species { get; set; }

        public ObservableCollection<List<Pokemon>> pokemons { get; set; }
        public ObservableCollection<List<PokemonSpecies>> pokemonSpecies { get; set; }

        public NationalPokedex(ViewsDependancy dependency)
        {
            species = new ObservableCollection<PaginatedResource<PokemonSpecies>>();
            species.Add(new PaginatedResource<PokemonSpecies>());
            pokemonSpecies = new ObservableCollection<List<PokemonSpecies>>();
            pokemonSpecies.Add(new List<PokemonSpecies>());
            pokemons = new ObservableCollection<List<Pokemon>>();
            pokemons.Add(new List<Pokemon>());
            InitializeComponent();
            this.DataContext = this;
            _PokeAPIClient = dependency.pokeAPIClient;
            GetSpecies();
            PageSelect.SelectedIndex = 0;
        }

        private async void GetSpecies(string uri = null)
        {
            PaginatedResource<PokemonSpecies> pr = await _PokeAPIClient.FetchPaginatedResource<PokemonSpecies>(uri ?? "https://pokeapi.co/api/v2/pokemon-species");
            species[0] = pr;
            PageSelect.ItemsSource = Enumerable.Range(1, species[0].totalPages).ToArray();
            PageSelect.SelectedIndex = species[0].page - 1;
            List<PokemonSpecies> pkmSpecies = await _PokeAPIClient.FetchListResource(species[0].Results);
            pokemons[0] = await _PokeAPIClient.FetchListPokemonFromPokemonApiModel(await _PokeAPIClient.FetchListResource(pkmSpecies.Select(q => q.Varieties.Where(v => v.Is_Default).First().Pokemon).ToList()));
            for (int i = 0; i < pkmSpecies.Count; i++)
            {
                pkmSpecies[i].defaultVariety = pokemons[0][i];
            }
            pokemonSpecies[0] = pkmSpecies;
        }

        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
            GetSpecies(species[0].next);
        }
        private void Menu_Btn_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateTo(l => new Menu(l));
        }
        private void Prev_Btn_Click(object sender, RoutedEventArgs e)
        {
            GetSpecies(species[0].previous);
        }

        public void GoToPokemonPage(object sender, RoutedEventArgs e)
        {
            if((PokemonSpecies)((Button)sender).Tag!=null)
                ((App)Application.Current).NavigateTo(l => new PokemonInfoView(l, (PokemonSpecies)((Button)sender).Tag));
        }

        private void PageSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetSpecies(species[0].GetLinkToPage((int)((ComboBox)sender).SelectedValue));
        }
    }
}
