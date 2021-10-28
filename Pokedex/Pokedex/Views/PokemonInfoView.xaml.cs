using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interação lógica para PokemonInfoView.xam
    /// </summary>
    public partial class PokemonInfoView : Page
    {
        public ObservableCollection<PokemonSpecies> _PokemonSpecies { get; set; }
        public PokemonInfoView(ViewsDependancy dependency, PokemonSpecies pkmSp)
        {
            _PokemonSpecies = new ObservableCollection<PokemonSpecies>();
            _PokemonSpecies.Add(pkmSp);
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
