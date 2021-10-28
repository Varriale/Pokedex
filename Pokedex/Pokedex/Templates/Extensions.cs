using Pokedex.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pokedex.Templates
{
    public class Extensions
    {
        public static readonly DependencyProperty PokemonProperty =
        DependencyProperty.RegisterAttached("Pokemon", typeof(Pokemon), typeof(Extensions), new FrameworkPropertyMetadata(default(Pokemon)));

        public static void SetPokemon(UIElement element, Pokemon value)
        {
            element.SetValue(PokemonProperty, value);
        }

        public static Pokemon GetPokemon(UIElement element)
        {
            return (Pokemon)element.GetValue(PokemonProperty);
        }


        public static readonly DependencyProperty PokemonSpeciesProperty =
            DependencyProperty.RegisterAttached("PokemonSpecies", typeof(PokemonSpecies), typeof(Extensions), new PropertyMetadata(default(PokemonSpecies)));

        public static void SetPokemonSpecies(UIElement element, PokemonSpecies value)
        {
            element.SetValue(PokemonSpeciesProperty, value);
        }

        public static PokemonSpecies GetPokemonSpecies(UIElement element)
        {
            return (PokemonSpecies)element.GetValue(PokemonSpeciesProperty);
        }
    }
}
