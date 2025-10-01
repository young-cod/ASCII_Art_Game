using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class GameManager : Singleton<GameManager>
    {
        public enum EGAMESTATE
        {
            Main, Playing,Pause, GameOver
        }
        public static EGAMESTATE State { get; set; }

        public GameManager(){
            Init();
        }

        public override void Init()
        {
            State = EGAMESTATE.Main;
            SceneManager.Instance.Init();
        }


    }
}
