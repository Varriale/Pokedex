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
        private Gender trainerGender;
        private DatabaseContext dbContext;
        public NewTrainerView(ViewsDependancy dependency)
        {
            InitializeComponent();
            dbContext = dependency.dbContext;
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
            trainerGender = Gender.Girl;
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
            ((App)Application.Current).NavigateTo(l => new TrainerSelection(l));
        }

        private async void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (TrainerName.Text == ""|| TrainerName.Text == "Name cant be empty")
            {
                TrainerName.Text = "Name cant be empty";
            }
            else
            {
                Trainer t = new Trainer { Gender = trainerGender, Name = TrainerName.Text };
                dbContext.Trainers.Add(t);
                await dbContext.SaveChangesAsync();
                ((App)Application.Current).NavigateTo(l => new TrainerSelection(l));
            }
        }
    }
}
