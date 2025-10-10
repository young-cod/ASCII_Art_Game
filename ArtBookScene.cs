
using System;
using System.Linq;

namespace AsciiArt
{
    class ArtBookScene : Scene, IInputHandler
    {
        int TitlePosX;
        int TitlePosY;

        const int DataStartPosX = 5;
        const int DataStartPosY = 10;

        int artPosX;
        const int artPosY = 10;

        int selectPosX = 0;
        int selectPosY = 0;
        const string selectStr = "▶";

        readonly string[] TitleArtString = new string[] {
               " __      _         _     ____              _      __ ",
               "| _|    / \\   _ __| |_  | __ )  ___   ___ | | __ |_ |",
               "| |    / _ \\ | '__| __| |  _ \\ / _ \\ / _ \\| |/ /  | |",
               "| |   / ___ \\| |  | |_  | |_) | (_) | (_) |   <   | |",
               "| |  /_/   \\_\\_|   \\__| |____/ \\___/ \\___/|_|\\_\\  | |",
               "|__|                                             |__|"
        };

        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectPosY > 0) selectPosY--;
                    break;
                case ConsoleKey.DownArrow:
                    if (selectPosY < (int)PokemonData.Name.Count - 1) selectPosY++;
                    break;
                case ConsoleKey.Escape:
                    SceneManager.Instance.LoadScene(EType.Main);
                    break;
                default:
                    break;
            }
        }

        public override void Init()
        {
            GameManager.Instance.watch.Restart();

            Type = EType.ArtBook;
            InputManager.Instance.SetHandler(this);
            //GameManager.Instance.State = GameManager.EGameState.Pause;

            TitlePosX = ScreenBuffer.currentW / 2 - (TitleArtString.Max(line => line.Length) / 2);
            TitlePosY = 3;

            artPosX = ScreenBuffer.currentW / 2;

            selectPosX = DataStartPosX - 3;
        }

        public override void Render()
        {
            ScreenBuffer.Draw(TitlePosX, TitlePosY, TitleArtString);

            ScreenBuffer.Draw(selectPosX, selectPosY, selectStr);

            int idx = 1;
            int posY = DataStartPosY;
            foreach (var pokemon in PokemonData.AllPokemon)
            {
                string name = pokemon.Value.MaxScore < pokemon.Value.UnlockScore ? new string('?', pokemon.Value.Name.Length) : pokemon.Value.Name;
                string data = $"{idx++} : {name}";
                ScreenBuffer.Draw(DataStartPosX, posY += 5, data);

            }

            Pokemon selectPokemon = PokemonData.AllPokemon[(PokemonData.Name)selectPosY];
            //해금됐다면
            if (selectPokemon?.MaxScore >= selectPokemon?.UnlockScore)
            {
                ScreenBuffer.Draw(artPosX, artPosY, selectPokemon.ArtLine);
            }
            else
            {
                ScreenBuffer.Draw(artPosX, artPosY, ArtManager.Instance.ArtToBlur(selectPokemon.ArtLine));
            }
        }
    }
}
