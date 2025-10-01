using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    enum EARTTYPE
    {
        Pokemon, Digimon
    }

    abstract class Art
    {
        enum Pokemon {Pikachu,Namobbemi,count }
        enum Digimon {count }

        public string[] BlurArt { get; set; }
        public int WIDTH { get; }
        public int HEIGHT { get; }

        public Art(string[] data)
        {
            if (data == null || data.Length == 0)
            {
                WIDTH = 0;
                HEIGHT = 0;
            }
            else
            {
                //가장 긴 라인 너비로
                WIDTH = data.Max(line => line.Length);
                HEIGHT = data.Length;
            }
        }

        abstract public string[] GetBlurArt();
    }
}
