using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    class MainScene : Scene, IInputHandler
    {
        public enum EMENU { Playing, ArtBook, Achievement, End, count }
        EMENU Menu { get; set; }
        readonly string[] title = new string[] {
" _____                                                                                                  _____ ",
"( ___ )                                                                                                ( ___ )",
" |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | ",
" |   |                                                                                                  |   | ",
" |   |                                                                                                  |   | ",
" |   |        _                           _          _   _                 _   _                _       |   | ",
" |   |       / \\   _ __ _ __ _____      _( )___     | | | | ___  __ _ _ __| |_| |__   ___  __ _| |_     |   | ",
" |   |      / _ \\ | '__| '__/ _ \\ \\ /\\ / /// __|    | |_| |/ _ \\/ _` | '__| __| '_ \\ / _ \\/ _` | __|    |   | ",
" |   |     / ___ \\| |  | | | (_) \\ V  V /  \\__ \\    |  _  |  __/ (_| | |  | |_| |_) |  __/ (_| | |_     |   | ",
" |   |    /_/   \\_\\_|  |_|  \\___/ \\_/\\_/   |___/    |_| |_|\\___|\\__,_|_|   \\__|_.__/ \\___|\\__,_|\\__|    |   | ",
" |   |                                                                                                  |   | ",
" |   |                                                                                                  |   | ",
" |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| ",
"(_____)                                                                                                (_____)",
        };
        readonly string startString = "시작하기";
        readonly string artBook = "아트북";
        readonly string achievement = "업적";
        readonly string endString = "끝내기";
        readonly string[] selectStr = new string[] { "▶", "◀" };

        // 각 문자열 위치
        int selectPosY;

        int startPosY;
        int artBookPosY;
        int achievementPosY;
        int endPosY;

        //인풋핸들러
        public bool IsHandlerInput => true;

        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Menu > 0) Menu--;
                    break;
                case ConsoleKey.DownArrow:
                    if (Menu < EMENU.count - 1) Menu++;
                    break;
                case ConsoleKey.Enter:
                    SceneManager.Instance.LoadScene((Scene.ETYPE)Menu+1);
                    break;
            }
            SceneManager.Instance.ClearScreenByFilling();
        }

        private int GetMenuPosY(EMENU menu)
        {
            switch (menu)
            {
                case EMENU.Playing: return startPosY;
                case EMENU.ArtBook: return artBookPosY;
                case EMENU.Achievement: return achievementPosY;
                case EMENU.End: return endPosY;
                default: return selectPosY;
            }
        }

        public override void Init()
        {
            Menu = EMENU.Playing;

            selectPosY = title.Length + 15;
            startPosY = selectPosY;
            artBookPosY = startPosY + 3;
            achievementPosY = artBookPosY + 3;
            endPosY = achievementPosY + 3;

            InputManager.Instance.SetHandler(this);
        }

        public override void Render()
        {
            //타이틀 아스키아트 렌더링
            for (int i = 0; i < title.Length; i++)
            {
                Tools.WriteLineAt(Tools.GetCenterPosX(title[i]), 7 + i, title[i]);
            }

            int windowCenterX = Console.WindowWidth / 2 - 4;

            //선택된 문자열 위치 렌더링
            Tools.WriteAt(windowCenterX - 10, GetMenuPosY(Menu), selectStr[0]);
            Tools.WriteAt(windowCenterX + 15, GetMenuPosY(Menu), selectStr[1]);

            //시작하기 문자열 위치 렌더링
            Tools.WriteLineAt(windowCenterX, startPosY, startString);

            //아트북 문자열 위치 렌더링
            Tools.WriteLineAt(windowCenterX, artBookPosY, artBook);

            //업적 문자열 위치 렌더링
            Tools.WriteLineAt(windowCenterX, achievementPosY, achievement);

            //끝내기 문자열 위치 렌더링
            Tools.WriteLineAt(windowCenterX, endPosY, endString);
        }
    }
}
