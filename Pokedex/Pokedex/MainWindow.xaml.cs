using Pokedex.Entities;
using Pokedex.Services;
using Pokedex.Views;
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
using System.Windows.Media.Animation;
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
        bool closed = true;
        public MainWindow()
        {
            InitializeComponent();
            //((App)Application.Current).NavigateTo<HomeView>();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (closed)
            {
                OpeningAnimation();
            }
            else
            {
                ClosingAnimation();
            }
            
        }
        private void OpeningAnimation()
        {
            double animdur = 600;
            DoubleAnimation turnAnimation = new DoubleAnimation(-90, 0, TimeSpan.FromSeconds(animdur / 1000), FillBehavior.HoldEnd);
            turnAnimation.EasingFunction = new CubicEase();
            this.RegisterName("RottTransform1", RottTransform);
            Storyboard.SetTargetName(turnAnimation, "RottTransform1");
            Storyboard.SetTargetProperty(turnAnimation,
                new PropertyPath(RotateTransform.AngleProperty));

            double animdur2 = 1400;

            ThicknessAnimation openAnimationWhite = new ThicknessAnimation(new Thickness(375,0,0,0), new Thickness(0), TimeSpan.FromSeconds(animdur2 / 1000), FillBehavior.HoldEnd);
            ThicknessAnimation openAnimationRed = new ThicknessAnimation(new Thickness(0, 0, 375, 0), new Thickness(0), TimeSpan.FromSeconds(animdur2 / 1000), FillBehavior.HoldEnd);
            openAnimationWhite.EasingFunction = new CubicEase();
            openAnimationRed.EasingFunction = new CubicEase();
            this.RegisterName("PkballWhite1", PkballWhite);
            Storyboard.SetTargetName(openAnimationWhite, "PkballWhite");
            Storyboard.SetTargetProperty(openAnimationWhite,
                new PropertyPath(Grid.MarginProperty));
            this.RegisterName("PkballRed1", PkballRed);
            Storyboard.SetTargetName(openAnimationRed, "PkballRed");
            Storyboard.SetTargetProperty(openAnimationRed,
                new PropertyPath(Grid.MarginProperty));


            Storyboard openAnimationStoryboard = new Storyboard();
            openAnimationStoryboard.Children.Add(openAnimationWhite);
            openAnimationStoryboard.Children.Add(openAnimationRed);

            turnAnimation.Completed += (_, _) => openAnimationStoryboard.Begin(this);
            
            Storyboard turnAnimationStoryboard = new Storyboard();
            turnAnimationStoryboard.Children.Add(turnAnimation);
            turnAnimationStoryboard.Begin(this);

            closed = false;
        }
        private void ClosingAnimation()
        {
            double animdur = 600;
            DoubleAnimation turnAnimation = new DoubleAnimation(0, -90, TimeSpan.FromSeconds(animdur / 1000), FillBehavior.HoldEnd);
            turnAnimation.EasingFunction = new CubicEase();
            this.RegisterName("RottTransform1", RottTransform);
            Storyboard.SetTargetName(turnAnimation, "RottTransform1");
            Storyboard.SetTargetProperty(turnAnimation,
                new PropertyPath(RotateTransform.AngleProperty));

            double animdur2 = 1400;

            ThicknessAnimation closeAnimationWhite = new ThicknessAnimation(new Thickness(0), new Thickness(375, 0, 0, 0), TimeSpan.FromSeconds(animdur2 / 1000), FillBehavior.HoldEnd);
            ThicknessAnimation closeAnimationRed = new ThicknessAnimation(new Thickness(0), new Thickness(0, 0, 375, 0), TimeSpan.FromSeconds(animdur2 / 1000), FillBehavior.HoldEnd);
            closeAnimationWhite.EasingFunction = new CubicEase();
            closeAnimationRed.EasingFunction = new CubicEase();
            this.RegisterName("PkballWhite1", PkballWhite);
            Storyboard.SetTargetName(closeAnimationWhite, "PkballWhite");
            Storyboard.SetTargetProperty(closeAnimationWhite,
                new PropertyPath(Grid.MarginProperty));
            this.RegisterName("PkballRed1", PkballRed);
            Storyboard.SetTargetName(closeAnimationRed, "PkballRed");
            Storyboard.SetTargetProperty(closeAnimationRed,
                new PropertyPath(Grid.MarginProperty));

            turnAnimation.Completed += (_, _) => this.Close();

            Storyboard turnAnimationStoryboard = new Storyboard();
            turnAnimationStoryboard.Children.Add(turnAnimation);
            closeAnimationRed.Completed += (_, _) => turnAnimationStoryboard.Begin(this);

            Storyboard closeAnimationStoryboard = new Storyboard();
            closeAnimationStoryboard.Children.Add(closeAnimationWhite);
            closeAnimationStoryboard.Children.Add(closeAnimationRed);
            closeAnimationStoryboard.Begin(this);

            closed = true;
        }

        private void Pkball_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
