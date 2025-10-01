using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    partial class Pokemon : Art
    {
        public Pokemon(string[] data) : base(data)
        {
        }

        public override string[] GetBlurArt()
        {
            throw new NotImplementedException();
        }
    }

}
