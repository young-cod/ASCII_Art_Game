using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AsciiArt
{
    internal class GameScene : Scene, IInputHandler
    {
        const int TimerPosX = 5;
        const int TimerPosY = 2;

        Queue<ArrowData.EType> arrowQueue = new Queue<ArrowData.EType>();
        ArrowGame arrowGame;
        public override void Init()
        {
            Type = EType.Game;
            InputManager.Instance.SetHandler(this);

            //게임 세팅
            arrowGame = new ArrowGame(1);
            arrowGame.Init();
        }


        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.UpArrow:
                case ConsoleKey.RightArrow:
                case ConsoleKey.DownArrow:
                    //맞혔다면
                    if (arrowGame.CheckInputArrow(keyInfo.Key))
                    {
                        //맞췄다면 뭘할까?
                    }
                    //틀렸다면
                    else
                    {
                        //게임 오버
                    }
                    break;
                case ConsoleKey.Escape:
                    SceneManager.Instance.LoadScene(EType.Main);
                    GameManager.Instance.State = GameManager.EGameState.Main;
                    break;
                default: return;
            }
        }

        public override void Render()
        {
            GameArea();

            //구분선
            for (int i = 1; i < ScreenBuffer.WIN_MAX_HEIGHT - 1; i++)
            {
                Tools.WriteLineAt(ScreenBuffer.WIN_MAX_WIDTH / 2 - 10, i, "|");
            }

            ArtArea();

        }

        private void GameArea()
        {
            //아직 게임 시작이 안됐다면
            if (GameManager.Instance.State != GameManager.EGameState.Playing)
            {
                float startTimer = ArrowGame.ReadyTimer - (arrowGame.watch.ElapsedMilliseconds / 1000.0f);
                Tools.WriteLineAt(TimerPosX, TimerPosY, $"{startTimer:0.0}초 후 게임이 시작됩니다.");

                //게임 시작
                if (startTimer <= 0)
                {
                    GameManager.Instance.State = GameManager.EGameState.Playing;

                    arrowGame.watch.Restart();
                }
            }
            else
            {
                float endTimer = ArrowGame.GameOverTimer - (arrowGame.watch.ElapsedMilliseconds / 1000.0f);
                Tools.WriteLineAt(TimerPosX, TimerPosY, $"남은 시간 : {endTimer:0.0}");

                ////화살표 리스트 출력
                //for (int i = 5; i < arrowGame.GetList().Count; i+=10)
                //{
                //    ScreenBuffer.Draw(i, 5, ArrowData.Check);
                //}

                ////체크 리스트 출력
                //for (int i = 5; i < arrowGame.Answer; i+=10)
                //{
                //    ScreenBuffer.Draw(i, 5, ArrowData.Check);
                //}
            }
        }

        private void ArtArea()
        {
            string[] artLine = PokemonData.AllPokemon["Namolbbami"].ArtLine;
            int height = PokemonData.AllPokemon["Namolbbami"].ArtLine.Length;
            for (int i = 2; i < height + 2; i++)
            {
                Tools.WriteLineAt(ScreenBuffer.WIN_MAX_WIDTH / 2 - 5, i, artLine[i]);

            }
        }
    }
}
