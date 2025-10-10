using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    enum ArtDiscoveryState
    {
        Locked, Discovered
    }



    abstract class Art
    {
        public int WIDTH { get; }
        public int HEIGHT { get; }

        public string Name { get; }
        public int UnlockScore { get; }
        public int MaxScore { get; }
        public string[] ArtLine { get; }

        public ArtDiscoveryState State { get; set; } = ArtDiscoveryState.Locked;

        public Art(string name, int unlockScore, int maxScore, string[] artLine)
        {
            Name = name;
            UnlockScore = unlockScore;
            ArtLine = artLine;
            MaxScore = maxScore;
        }
    }
}
