using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class Pokemon : Art
    {
        public Pokemon(string name, int unlockScore,int maxScore, string[] artLine) : base(name, unlockScore,maxScore, artLine) { }
    }
}
