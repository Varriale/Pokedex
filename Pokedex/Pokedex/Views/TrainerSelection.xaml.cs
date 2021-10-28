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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para TrainerSelection.xam
    /// </summary>
    public partial class TrainerSelection : Page
    {
        private DatabaseContext dbContext;

        public TrainerSelection(ViewsDependancy dependency)
        {
            InitializeComponent();
            dbContext = dependency.dbContext;
            GetTrainers();
        }

        private void GetTrainers()
        {
            //dbContext.Database.EnsureCreated();

            List<Trainer> trainers = dbContext.Trainers.ToList();
            foreach (Trainer t in trainers)
            {

                Button b = new Button();

                StackPanel p = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Width = 200,
                    VerticalAlignment = VerticalAlignment.Center
                };

                Image profilePic = new Image
                {
                    Height = 200,
                    Margin = new Thickness(5)
                };
                if (t.Gender == Gender.Boy)
                    profilePic.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Spr_FRLG_Red.png"));
                else
                    profilePic.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Spr_FRLG_Leaf.png"));
                p.Children.Add(profilePic);

                TextBlock textB = new TextBlock();
                textB.TextAlignment = TextAlignment.Center;
                textB.Text = t.Name;
                textB.FontFamily = new FontFamily("Arial Black");
                textB.TextWrapping = TextWrapping.WrapWithOverflow;
                textB.FontSize = 24;
                p.Children.Add(textB);

                b.Content = p;
                TrainerListPanel.Children.Add(b);

                b.Tag = t;
                b.Click += TrainerClicked;
            }
            //NEW TRAINER BUTTON
            Button nb = new Button();

            StackPanel np = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Width = 200,
                VerticalAlignment = VerticalAlignment.Center
            };


            TextBlock plusIcon = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Height = 200,
                Text = "+",
                FontSize = 100,
            };
            np.Children.Add(plusIcon);

            TextBlock ntextB = new TextBlock
            {
                TextAlignment = TextAlignment.Center,
                Text = "New Trainer",
                FontFamily = new FontFamily("Arial Black"),
                TextWrapping = TextWrapping.WrapWithOverflow,
                FontSize = 24
            };
            np.Children.Add(ntextB);

            nb.Content = np;
            TrainerListPanel.Children.Add(nb);

            nb.Click += NewTrainerClicked;
        }

        private void TrainerClicked(object sender, RoutedEventArgs e)
        {
            Trainer t = (Trainer)((Button)sender).Tag;
            ((App)Application.Current).SetTrainer(t);
            ((App)Application.Current).NavigateTo(l => new Menu(l));

        }

        private void NewTrainerClicked(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateTo(l => new NewTrainerView(l));

        }
    }
}
