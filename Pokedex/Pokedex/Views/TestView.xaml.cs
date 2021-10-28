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

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para TestView.xam
    /// </summary>
    public partial class TestView : Page
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        private DatabaseContext dbContext;
        public TestView(ViewsDependancy dependency)
        {
            dbContext = dependency.dbContext;
            _PokeAPIClient = dependency.pokeAPIClient;
            InitializeComponent();
            GetTrainers();
        }
        private void GetTrainers()
        {
            dbContext.Database.EnsureCreated();
            var trainers = dbContext.Trainers.ToList();
            TrainerDG.ItemsSource = trainers;
            TrainerNameTxt.Text = ((App)Application.Current).CurrentTrainer.Name;
        }

        private async void showLangButton_Click(object sender, RoutedEventArgs e)
        {
            NamedAPIResource<Language> langEn = new NamedAPIResource<Language> { Name = "en", Url = "https://pokeapi.co/api/v2/language/9/" };
            Language en = await _PokeAPIClient.FetchResource(langEn);
            LabelEn.Content = en.Name;

            NamedAPIResource<PokemonApiModel> SpeciesBulba = new NamedAPIResource<PokemonApiModel> { Name = "bulbasaur", Url = "https://pokeapi.co/api/v2/pokemon/1" };
            PokemonApiModel bulbasaur = await _PokeAPIClient.FetchResource(SpeciesBulba);

        }
        private void HomePageClick(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateTo(l => new HomeView(l));

        }
    }
}
