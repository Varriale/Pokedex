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
    /// Interação lógica para Menu.xam
    /// </summary>
    public partial class Menu : Page
    {
        public Menu(ViewsDependancy dependency)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            ((App)Application.Current).NavigateTo(l => new NationalPokedex(l));
        }
    }
}
