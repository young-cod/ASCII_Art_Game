using System;
using System.Collections.Generic;
using System.Threading;

namespace AsciiArt
{
    internal class GameScene : Scene, IInputHandler
    {
        const int TIMERPOSX = 5;
        const int TIMERPOSY = 2;
        const int ARROWGAP = 10;
        const int ARROW_START_POSX = 5;
        const int ARROW_START_POSY = 5;

        readonly int gameArea_Width;
        readonly int artArea_Width;

        Queue<ArrowData.EType> arrowQueue = new Queue<ArrowData.EType>();
        ArrowGame arrowGame;
        public override void Init()
        {
            Type = EType.Game;
            InputManager.Instance.SetHandler(this);
            //GameManager.Instance.State = GameManager.EGameState.Pause;

            //게임 세팅
            arrowGame = new ArrowGame(1);
            arrowGame.Init();
        }


        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            //Console.WriteLine(GameManager.Instance.State);
            //게임 시작하기 전까지 입력 무시
            if (GameManager.Instance.State != GameManager.EGameState.Playing)
            {
                return;
            }
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
                        SceneManager.Instance.LoadScene(EType.GameOver);
                    }
                    break;
                case ConsoleKey.Escape:
                    SceneManager.Instance.LoadScene(EType.Main);
                    break;
                default: return;
            }
        }

        public override void Render()
        {
            GameArea();

            //구분선
            for (int i = 2; i < ScreenBuffer.currentH - 2; i++)
            {
                Tools.WriteLineAt(ScreenBuffer.currentW/ 2 - 4, i, "|");
            }

            ArtArea();

        }

        private void GameArea()
        {
            //아직 게임 시작이 안됐다면
            if (GameManager.Instance.State != GameManager.EGameState.Playing)
            {
                float startTimer = ArrowGame.ReadyTimer - (arrowGame.watch.ElapsedMilliseconds / 1000.0f);
                //Tools.WriteLineAt(TIMERPOSX, TIMERPOSY, $"{startTimer:0.0}초 후 게임이 시작됩니다.");
                ScreenBuffer.Draw(TIMERPOSX, TIMERPOSY, $"{startTimer:0.0}초 후 게임이 시작됩니다.");

                //게임 시작
                if (startTimer <= 0)
                {
                    GameManager.Instance.State = GameManager.EGameState.Playing;

                    arrowGame.watch.Restart();
                    SceneManager.Instance.ClearScreenByFilling();
                }
            }
            else
            {
                float endTimer = ArrowGame.GameOverTimer - (arrowGame.watch.ElapsedMilliseconds / 1000.0f);
                Tools.WriteLineAt(TIMERPOSX, TIMERPOSY, $"남은 시간 : {endTimer:0.0}");
                //ScreenBuffer.Draw(TIMERPOSX, TIMERPOSY, $"남은 시간 : {endTimer:0.0}");

                //ScreenBuffer.Draw(5, 5, ArrowData.LeftArrow);

                //화살표 리스트 출력
                int posY = ARROW_START_POSY;
                for (int i = 0; i < GameManager.Instance.arrowList.Count; i++)
                {
                    //Debug.Log(GameManager.Instance.arrowList.Count,2);
                    ArrowData.EType type = GameManager.Instance.arrowList[i];
                    int posX = ARROW_START_POSX + ((i % ArrowGame.LINE_MAXCOUNT) * ARROWGAP);
                    if (i != 0 && i % ArrowGame.LINE_MAXCOUNT == 0)
                    {
                        posY += ARROW_START_POSY;
                    }
                    //Debug.Log($"{posX},{posY}");
                    //Console.ForegroundColor = ConsoleColor.Red;
                    //Tools.ArtLineAllRenderAt(posX, posY, ArrowData.GetArrowData(type));
                    ScreenBuffer.Draw(posX, posY, ArrowData.GetArrowData(type));
                    //Console.ResetColor();
                }

                //Debug.Log(arrowGame.Answer);

                //체크 리스트 출력
                int posCheckY = ARROW_START_POSY;
                for (int i = 0; i < arrowGame.Answer; i++)
                {
                    int posX = ARROW_START_POSX + ((i % ArrowGame.LINE_MAXCOUNT) * ARROWGAP);
                    if (i != 0 && i % ArrowGame.LINE_MAXCOUNT == 0)
                    {
                        posCheckY += ARROW_START_POSY;
                    }
                    //Console.ForegroundColor = ConsoleColor.Green;
                    //Tools.ArtLineAllRenderAt(posX, posCheckY, ArrowData.Check);
                    ScreenBuffer.Draw(posX, posCheckY, ArrowData.Success,ConsoleColor.Green);
                    //Console.ResetColor();
                }
            }
        }

        private void ArtArea()
        {
            string[] artLine = PokemonData.AllPokemon["Namolbbami"].ArtLine;
            int height = PokemonData.AllPokemon["Namolbbami"].ArtLine.Length;
            for (int i = 2; i < height + 2; i++)
            {
                Tools.WriteLineAt(ScreenBuffer.currentW / 2 - 5, i, artLine[i]);

            }
        }
    }
}
