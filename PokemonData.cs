using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal static class PokemonData
    {
        public static readonly Dictionary<string, Pokemon> AllPokemon;

        //처음 호출될때 정적생성자 호출
        static PokemonData()
        {
            AllPokemon = new Dictionary<string, Pokemon>
            {
                ["Namolbbami"] = new Pokemon(
                    name: "나몰빼미",
                    unlockScore: 50,
                    artLine: new string[] { 
             
                    }
                ),
            };
        }
    }
}
