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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para Menu.xam
    /// </summary>
    public partial class Menu : Page
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        int colNum = 8;
        int rowNum = 5;
        public Menu(ViewsDependancy dependency)
        {
            _PokeAPIClient = dependency.pokeAPIClient;
            InitializeComponent();
            //Drawing to black Animation
            
            for (int j = 1; j <= colNum; j++)
            {
                AnimationSquaresGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 1; i <= rowNum; i++)
            {
                AnimationSquaresGrid.RowDefinitions.Add(new RowDefinition());
            }
            Panel.SetZIndex(AnimationSquaresGrid, -1);
            NameScope.SetNameScope(this, new NameScope());
        }

        private void NatDex_Click(object sender, RoutedEventArgs e)
        {

            ((App)Application.Current).NavigateTo(l => new NationalPokedex(l));
        }

        private async void TallGrass_Click(object sender, RoutedEventArgs e)
        {
            double animdur = 200;//milliseconds
            Panel.SetZIndex(AnimationGrid, 1);
            Task<Pokemon> pkmTask = new Task<Pokemon>(() => { while (true) { }; return new Pokemon(); });

            List<Canvas> canvasList = new List<Canvas>();
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    Canvas c = new Canvas { Background = new SolidColorBrush(Colors.Black), Opacity = 0, Margin = new Thickness(-1) };
                    c.SetValue(Grid.RowProperty, i);
                    c.SetValue(Grid.ColumnProperty, j);
                    canvasList.Add(c);
                    AnimationSquaresGrid.Children.Add(c);
                }
            }

            DoubleAnimation fadeAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(animdur / 1000), FillBehavior.Stop);

            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    StartDelayedAnimation((int)animdur / 2 * (i * colNum + j), canvasList[i * colNum + j], fadeAnimation);
                }
            }
            DoubleAnimation fadeAnimationUltimo = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(animdur / 1000), FillBehavior.Stop);

            StartDelayedAnimation((int)animdur / 2 * (rowNum * colNum - 1), canvasList[rowNum * colNum - 1], fadeAnimationUltimo);

            PaginatedResource<PokemonSpecies> pr = await _PokeAPIClient.FetchPaginatedResource<PokemonSpecies>("https://pokeapi.co/api/v2/pokemon-species");
            Random rnd = new Random();
            int randPk = rnd.Next(1, pr.Count + 1);
            PokemonSpecies pkm = await _PokeAPIClient.FetchSpeciesByNumber(randPk);
            pkmTask = _PokeAPIClient.FetchPokemonFromPokemonApiModel(await _PokeAPIClient.FetchResource(pkm.Varieties.Where(v => v.Is_Default).First().Pokemon));

            double slideAnimdur = 1000d;
            DoubleAnimation slideAnimation = new DoubleAnimation(-1024, 0, TimeSpan.FromSeconds(slideAnimdur / 1000), FillBehavior.Stop);
            DoubleAnimation offsetAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(slideAnimdur / 2000), FillBehavior.Stop);
            offsetAnimation.BeginTime = TimeSpan.FromSeconds(slideAnimdur / 2000);
            DoubleAnimation offsetAnimation2 = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(slideAnimdur / 2000), FillBehavior.Stop);
            offsetAnimation2.BeginTime = TimeSpan.FromSeconds(slideAnimdur / 1000);


            this.RegisterName("GradientStop1", FadeWhiteStop1);
            this.RegisterName("GradientStop2", FadeWhiteStop2);
            Storyboard.SetTargetName(offsetAnimation, "GradientStop1");
            Storyboard.SetTargetProperty(offsetAnimation,
                new PropertyPath(GradientStop.OffsetProperty));
            Storyboard.SetTargetName(offsetAnimation2, "GradientStop2");
            Storyboard.SetTargetProperty(offsetAnimation2,
                new PropertyPath(GradientStop.OffsetProperty));
            // Create a Storyboard to apply the animations.
            Storyboard gradientStopAnimationStoryboard = new Storyboard();
            gradientStopAnimationStoryboard.Children.Add(offsetAnimation);
            //gradientStopAnimationStoryboard.Children.Add(offsetAnimation2);

            fadeAnimationUltimo.Completed += (_, _) =>
            {
                TallGrass1.BeginAnimation(Canvas.RightProperty, slideAnimation);
                TallGrass2.BeginAnimation(Canvas.LeftProperty, slideAnimation);
                TallGrass1.BeginAnimation(OpacityProperty, fadeAnimation);
                TallGrass2.BeginAnimation(OpacityProperty, fadeAnimation);
                fadeAnimation.Completed += (_, _) => { TallGrass1.Opacity = 1; TallGrass2.Opacity = 1; };
                gradientStopAnimationStoryboard.Begin(this);
            };

            gradientStopAnimationStoryboard.Completed += async (_, _) =>
            {
                pkm.defaultVariety = await pkmTask;
                ((App)Application.Current).NavigateTo(l => new PokemonInfoView(l, pkm), () =>
                {
                    foreach (Canvas canv in canvasList)
                    {
                        canv.Opacity = 0;
                    }
                    Panel.SetZIndex(AnimationGrid, -1);
                }
                );

            };
        }

        public static void StartDelayedAnimation(int millisecond, UIElement obj, DoubleAnimation fadeAnimation)
        {
            var timer = new DispatcherTimer();
            timer.Tick += delegate

            {
                fadeAnimation.Completed += (_, _) => { obj.Opacity = 1; };
                obj.BeginAnimation(OpacityProperty, fadeAnimation);
                timer.Stop();
            };

            timer.Interval = TimeSpan.FromMilliseconds(millisecond);
            timer.Start();
        }
    }
}
