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
    /// Interação lógica para HomeView.xam
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void TestPageClick(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateTo<TestView>();
            //IServiceProvider SP = ((App)Application.Current).serviceProvider;
            //TestView tv = new TestView(SP.GetService(IPokeAPIClient), SP.GetService(DatabaseContext));
        }
    }
}
