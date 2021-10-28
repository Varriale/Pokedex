using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Pokedex.Entities
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Color Color { get { return GetColor();}}

        public Color GetColor()
        {
            Color typeColor;
            typeColor.A = 255;
            switch (Id)
            {
                case 1://Normal
                    typeColor.R = 168;
                    typeColor.G = 168;
                    typeColor.B = 120;
                    break;
                case 2://fighting
                    typeColor.R = 192;
                    typeColor.G = 48;
                    typeColor.B = 40;
                    break;
                case 3://flying
                    typeColor.R = 168;
                    typeColor.G = 144;
                    typeColor.B = 240;
                    break;
                case 4://poison
                    typeColor.R = 160;
                    typeColor.G = 64;
                    typeColor.B = 160;
                    break;
                case 5://ground
                    typeColor.R = 224;
                    typeColor.G = 192;
                    typeColor.B = 104;
                    break;
                case 6://rock
                    typeColor.R = 184;
                    typeColor.G = 160;
                    typeColor.B = 56;
                    break;
                case 7://bug
                    typeColor.R = 168;
                    typeColor.G = 184;
                    typeColor.B = 32;
                    break;
                case 8://ghost
                    typeColor.R = 112;
                    typeColor.G = 88;
                    typeColor.B = 152;
                    break;
                case 9://steel
                    typeColor.R = 184;
                    typeColor.G = 184;
                    typeColor.B = 208;
                    break;
                case 10://fire
                    typeColor.R = 240;
                    typeColor.G = 128;
                    typeColor.B = 48;
                    break;
                case 11://water
                    typeColor.R = 104;
                    typeColor.G = 144;
                    typeColor.B = 240;
                    break;
                case 12://grass
                    typeColor.R = 120;
                    typeColor.G = 200;
                    typeColor.B = 80;
                    break;
                case 13://electric
                    typeColor.R = 248;
                    typeColor.G = 208;
                    typeColor.B = 48;
                    break;
                case 14://psychic
                    typeColor.R = 248;
                    typeColor.G = 88;
                    typeColor.B = 136;
                    break;
                case 15://ice
                    typeColor.R = 152;
                    typeColor.G = 216;
                    typeColor.B = 216;
                    break;
                case 16://dragon
                    typeColor.R = 112;
                    typeColor.G = 56;
                    typeColor.B = 248;
                    break;
                case 17://dark
                    typeColor.R = 112;
                    typeColor.G = 88;
                    typeColor.B = 72;
                    break;
                case 18://fairy
                    typeColor.R = 238;
                    typeColor.G = 153;
                    typeColor.B = 172;
                    break;
                case 10001://unknown
                    typeColor.R = 104;
                    typeColor.G = 160;
                    typeColor.B = 144;
                    break;
                case 10002://shadow
                    typeColor.R = 0;
                    typeColor.G = 0;
                    typeColor.B = 0;
                    break;

                default:
                    break;
            }
            return typeColor;
        }


    }
}
