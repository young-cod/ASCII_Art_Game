using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class GameManager : Singleton<GameManager>
    {
        public Stopwatch watch = new Stopwatch(); // 프레임 제어용 스톱워치
        public int Score { get; set; }

        public enum EGameState
        {
             Main, Playing,Pause, GameOver
        }
        public EGameState State { get; set; }

        public GameManager(){
            Init();
        }


        public override void Init()
        {
            watch.Start();
            State = EGameState.Main;
            
            //버퍼 초기화
            ScreenBuffer.Init();
            //매니저 초기화
            SceneManager.Instance.Init();
        }
       
    }
}
