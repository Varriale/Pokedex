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
using Pokedex.Entities;
using Pokedex.Services;

namespace Pokedex.Views
{
    /// <summary>
    /// Interação lógica para PokemonInfoView.xam
    /// </summary>
    public partial class PokemonInfoView : Page
    {
        private readonly IPokeAPIClient _PokeAPIClient;
        public ObservableCollection<PokemonSpecies> _PokemonSpecies { get; set; }
        public PokemonInfoView(ViewsDependancy dependency, PokemonSpecies pkmSp)
        {
            _PokeAPIClient = dependency.pokeAPIClient;
            _PokemonSpecies = new ObservableCollection<PokemonSpecies>();
            _PokemonSpecies.Add(pkmSp);
            InitializeComponent();
            this.DataContext = this;
            foreach (UIElement t in GetTypeTags(_PokemonSpecies[0].defaultVariety.Types))
                 Types.Children.Add(t);
            SetSpeciesTags();
            GenderGrid.Visibility = _PokemonSpecies[0].Has_Gender_differences ? Visibility.Visible : Visibility.Collapsed;
            GetVarietiesAsync();
            GetFormsAsync();
            GetEvolutions();
        }
        private List<UIElement> GetTypeTags(List<Entities.Type> Types)
        {
            List<UIElement> collect = new List<UIElement>();
            foreach (Entities.Type t in Types)
            {
                Border b = new Border
                {
                    Background = new SolidColorBrush { Color = t.Color },
                    Height = 30,
                    Width = 90,
                    Padding = new Thickness(10, 0, 10, 0),
                    Margin = new Thickness(2),
                    CornerRadius = new CornerRadius(15),
                    Child = new TextBlock
                    {
                        Text = t.Name.ToUpper(),
                        Foreground = new SolidColorBrush { Color = Colors.White },
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush { Color = Colors.White }
                };
                collect.Add(b);
            }
            return collect;
        }

        private void SetSpeciesTags()
        {
            if(_PokemonSpecies[0].Is_Baby)
            {
                Border b = new Border
                {
                    Background = new SolidColorBrush { Color = Colors.LightPink },
                    Height = 30,
                    Width = 80,
                    Padding = new Thickness(10, 0, 10, 0),
                    Margin = new Thickness(2),
                    CornerRadius = new CornerRadius(15),
                    Child = new TextBlock
                    {
                        Text = "Baby",
                        Foreground = new SolidColorBrush { Color = Colors.White },
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush { Color = Colors.White }
                };
                PkmTags.Children.Add(b);
            }
            if (_PokemonSpecies[0].Is_Mythical)
            {
                Border b = new Border
                {
                    Background = new SolidColorBrush { Color = Colors.Silver },
                    Height = 30,
                    Width = 80,
                    Padding = new Thickness(10, 0, 10, 0),
                    Margin = new Thickness(2),
                    CornerRadius = new CornerRadius(15),
                    Child = new TextBlock
                    {
                        Text = "Mythical",
                        Foreground = new SolidColorBrush { Color = Colors.White },
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush { Color = Colors.White }
                };
                PkmTags.Children.Add(b);
            }
            if (_PokemonSpecies[0].Is_Legendary)
            {
                Border b = new Border
                {
                    Background = new SolidColorBrush { Color = Colors.Gold },
                    Height = 30,
                    Width = 80,
                    Padding = new Thickness(10, 0, 10, 0),
                    Margin = new Thickness(2),
                    CornerRadius = new CornerRadius(15),
                    Child = new TextBlock
                    {
                        Text = "Legendary",
                        Foreground = new SolidColorBrush { Color = Colors.White },
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    BorderThickness = new Thickness(2),
                    BorderBrush = new SolidColorBrush { Color = Colors.White }
                };
                PkmTags.Children.Add(b);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).NavigateBack();
        }
        private async Task GetVarietiesAsync()
        {
            foreach(PokemonSpeciesVariety speciesVariety in _PokemonSpecies[0].Varieties)
            {
                if (!speciesVariety.Is_Default)
                {
                    VarietiesGrid.Visibility = Visibility.Visible;
                    Pokemon pkm = await _PokeAPIClient.FetchPokemonFromPokemonApiModel(await _PokeAPIClient.FetchResource(speciesVariety.Pokemon));
                    StackPanel sp = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin=new Thickness(5)
                    };
                    Border b = new Border { Child = sp, BorderThickness = new Thickness(1),BorderBrush=new SolidColorBrush(Colors.Black)};
                    sp.Children.Add(new TextBlock { Text = pkm.Name.Replace('-',' ').ToUpper(), FontSize = 24 });
                    StackPanel types = new StackPanel { Orientation = Orientation.Horizontal,HorizontalAlignment=HorizontalAlignment.Center };
                    foreach (UIElement t in GetTypeTags(pkm.Types))
                        types.Children.Add(t);
                    sp.Children.Add(types);

                    Image myImage = new Image();
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(pkm.OfficialArtworkOrDefault, UriKind.Absolute);
                    bi3.EndInit();
                    myImage.Source = bi3;
                    myImage.Height = 200;
                    sp.Children.Add(myImage);

                    VarietiesSP.Children.Add(b);
                }
            }
        }

        private async Task GetFormsAsync()
        {
            foreach (PokemonForm pkmForm in _PokemonSpecies[0].defaultVariety.Forms)
            {
                if (!pkmForm.Is_Default)
                {
                    FormsGrid.Visibility = Visibility.Visible;
                    //Pokemon pkm = await _PokeAPIClient.FetchPokemonFromPokemonApiModel(await _PokeAPIClient.FetchResource(pkmForm.Pokemon));
                    StackPanel sp = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(5)
                    };
                    List<Pokedex.Entities.Type> typesList = pkmForm.Types.Select(t => _PokeAPIClient.TypeFromName(t.Type.Name)).ToList();
                    Border b = new Border { Child = sp, BorderThickness = new Thickness(1), BorderBrush = new SolidColorBrush(Colors.Black) };
                    sp.Children.Add(new TextBlock { Text = pkmForm.enName, FontSize = 24 });
                    StackPanel types = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
                    foreach (UIElement t in GetTypeTags(typesList))
                        types.Children.Add(t);
                    sp.Children.Add(types);

                    Image myImage = new Image();
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(pkmForm.OfficialArtworkOrDefault, UriKind.Absolute);
                    bi3.EndInit();
                    myImage.Source = bi3;
                    myImage.Height = 200;
                    sp.Children.Add(myImage);

                    FormsSP.Children.Add(b);
                }
            }
        }
        

        private async Task GetEvolutions()
        {
            EvolutionChain evolutionChain = await _PokeAPIClient.FetchResource(_PokemonSpecies[0].Evolution_Chain);

            EvolutionTV.Items.Add(await GetTreeViewItem(evolutionChain.Chain));
            
        }

        private async Task<TreeViewItem> GetTreeViewItem(ChainLink chainLink)
        {
            PokemonSpecies pkm = await _PokeAPIClient.FetchResource(chainLink.Species);
            pkm.defaultVariety = await _PokeAPIClient.FetchPokemonFromPokemonApiModel(await _PokeAPIClient.FetchResource(pkm.Varieties.Where(v => v.Is_Default).First().Pokemon));
            Image myImage = new Image();
            BitmapImage bi3 = new BitmapImage();
            bi3.BeginInit();
            bi3.UriSource = new Uri(pkm.defaultVariety.OfficialArtworkOrDefault, UriKind.Absolute);
            bi3.EndInit();
            myImage.Source = bi3;
            myImage.Height = 175;
            Button stepButton = new Button();
            stepButton.Click += (sender, e) =>
            {
                ((App)Application.Current).NavigateTo(l => new PokemonInfoView(l, pkm));
            };
            StackPanel evolutionStep = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Top };
            stepButton.Content = evolutionStep;
            stepButton.Background = pkm.defaultVariety.TypesBrush;
            stepButton.BorderBrush = new SolidColorBrush(Colors.Transparent);
            StackPanel pokemonInfo = new StackPanel { Orientation = Orientation.Vertical, HorizontalAlignment=HorizontalAlignment.Center};
            pokemonInfo.Children.Add(new TextBlock { Text = pkm.enName, FontSize = 16, TextAlignment = TextAlignment.Center }) ;
            StackPanel types = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center };
            foreach (UIElement t in GetTypeTags(pkm.defaultVariety.Types))
                types.Children.Add(t);
            pokemonInfo.Children.Add(types);
            pokemonInfo.Children.Add(myImage);
            /*if(chainLink.Evolution_Details!=null&& chainLink.Evolution_Details.Count()!=0)
                evolutionStep.Children.Add(new TextBlock { Text = chainLink.Evolution_Details[0].Trigger.Name+" "+chainLink.Evolution_Details[0].Min_Level.ToString(),FontSize=16 });*/
            evolutionStep.Children.Add(pokemonInfo);
            TreeViewItem Item = new TreeViewItem {IsExpanded=true, Header = stepButton };
            
            foreach(ChainLink evTo in chainLink.Evolves_To)
            {
                Item.Items.Add(await GetTreeViewItem(evTo));
            }
            return Item;
            

        }
    }
}
