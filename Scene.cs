using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    abstract class Scene
    {
        public enum ETYPE { Main, Game, ArtBook, DetailArtBook, Achievement }

        abstract public void Render();
        abstract public void Init();

    }
}
