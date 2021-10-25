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

namespace Pokedex
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        private DatabaseContext dbContext;
        public MainWindow(IPokeAPIClient pokeAPIClient,DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
            _PokeAPIClient = pokeAPIClient;
            InitializeComponent();
            GetTrainers();
        }
        private void GetTrainers()
        {
            dbContext.Database.EnsureCreated();
            var trainers = dbContext.Trainers.ToList();
            TrainerDG.ItemsSource = trainers;
        }

        private async void showLangButton_Click(object sender, RoutedEventArgs e)
        {
            NamedAPIResource<Language> langEn = new NamedAPIResource<Language> { Name = "en", Url = "https://pokeapi.co/api/v2/language/9/" };
            Language en = await _PokeAPIClient.FetchResource(langEn);
            LabelEn.Content = en.Name;
        }

    }
}
