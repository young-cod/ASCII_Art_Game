using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    enum ArtDiscoveryState{
        Locked, Discovered
    }

    enum ArtType
    {
        Pokemon, Digimon
    }

    abstract class Art
    {
        public int WIDTH { get; }
        public int HEIGHT { get; }

        public string Name { get; }
        public int UnlockScore { get; }
        public string[] ArtLine { get; }

        public ArtDiscoveryState State { get; set; } = ArtDiscoveryState.Locked;

        public Art(string name, int unlockScore, string[] artLine){
            Name = name;
            UnlockScore = unlockScore;
            ArtLine = artLine;
        }
    }
}
