using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pokedex.Entities
{
    public class Pokemon
    {
        public string Name { get; set; }
        public List<PokemonForm> Forms { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }
        public int Weight { get; set; }
        public PokemonSprites Sprites { get; set; }
        public NamedAPIResource<PokemonSpecies> Species { get; set; }
        public List<Pokedex.Entities.Type> Types { get; set; }
        public string HeightStr { get
            {
                return (Height / 10.0).ToString() + " m";
            }
        }
        public string WeightStr
        {
            get
            {
                return (Weight / 10.0).ToString() + " kg";
            }
        }

        public LinearGradientBrush TypesBrush { get
            {
                LinearGradientBrush brush = new LinearGradientBrush();
                brush.Opacity = 0.5;
                for(int i=0;i<Types.Count;i++)
                    brush.GradientStops.Add(new GradientStop { Color = Types[i].Color, Offset = ((double)i)/(Types.Count==1?1: Types.Count-1)*0.5+.25 });
                return brush;
            }
        }

        public string OfficialArtworkOrDefault
        {
            get
            {
                if (Sprites.Other != null && Sprites.Other.ContainsKey("official-artwork"))
                    return Sprites.Other["official-artwork"].Front_Default;
                else
                    return Sprites.Front_Default;
            }
        }
    }
}
