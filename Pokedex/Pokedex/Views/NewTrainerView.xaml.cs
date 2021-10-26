using Pokedex.Entities;
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
    /// Interação lógica para NewTrainerView.xam
    /// </summary>
    public partial class NewTrainerView : Page
    {
        Gender trainerGender;
        public NewTrainerView()
        {
            InitializeComponent();
        }

        private void Rectangle_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Rectangle r = (Rectangle)sender;
            if((bool)e.NewValue)
                r.Opacity = 0;
            else
                r.Opacity = 1;
        }

        private void BoyBtn_Click(object sender, RoutedEventArgs e)
        {
            trainerGender = Gender.Boy;
            GirlBtn.Visibility = Visibility.Collapsed;
            CollapseGenerics();
        }
        private void GirlBtn_Click(object sender, RoutedEventArgs e)
        {
            trainerGender = Gender.Boy;
            BoyBtn.Visibility = Visibility.Collapsed;
            CollapseGenerics();
        }

        private void CollapseGenerics()
        {
            TrainerNameStep.Visibility = Visibility.Visible;
            profOak.Visibility = Visibility.Collapsed;
            BoyRedSillouete.Visibility = Visibility.Collapsed;
            GirlLeafSillouete.Visibility = Visibility.Collapsed;
            GenderQuestion.Visibility = Visibility.Collapsed;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateTo<TrainerSelection>();
        }
    }
}
