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

        public List<ArrowData.EType> arrowList = new List<ArrowData.EType>();

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

        public void SetArrowList(Queue<ArrowData.EType> queue){
            int count = queue.Count;
            //queue.Count로 넣으면 큐를 디큐 하면서 카운트도 줄어들기 때문에
            //미리 변수로 넣고 포문을 돌려야함
            //그렇지 않으면, 예상치 못한 오류!(즉, 1/2 밖에 안들어감!!!)주의!!!
            //for(int i=0; i<count; i++){
            //    arrowList.Add(queue.Dequeue());
            //}


            arrowList = queue.ToList();
        }
       
    }
}
