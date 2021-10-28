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

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para NationalPokedex.xam
    /// </summary>
    public partial class NationalPokedex : Page
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        public ObservableCollection<PaginatedResource<PokemonSpecies>> species { get; set; }

        public NationalPokedex(ViewsDependancy dependency)
        {
            species = new ObservableCollection<PaginatedResource<PokemonSpecies>>();
            InitializeComponent();
            this.DataContext = this;
            _PokeAPIClient = dependency.pokeAPIClient;
            GetSpecies();
        }

        private async void GetSpecies(string uri = null)
        {
            PaginatedResource<PokemonSpecies> pr = await _PokeAPIClient.FetchPaginatedResource<PokemonSpecies>(uri ?? "https://pokeapi.co/api/v2/pokemon-species");
            species.Clear();
            species.Add(pr);
            Btn2.Content = species[0].Results[0].Name;
        }

        private void Next_Btn_Click(object sender, RoutedEventArgs e)
        {
            GetSpecies(species[0].next);
        }
    }
}
