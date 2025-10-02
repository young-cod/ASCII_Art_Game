using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class ArrowGame
    {
        //todo : 레벨, 난이도, 점수 세팅하기
        public int Level { get; private set; }
        public int Score { get; private set; }

        //게임 시작 준비시간
        public const float ReadyTimer = 5f;
        public const float GameOverTimer = 5f;
        public const float GameEndTimer = 60f;
        //todo : 한 입력마다 제한시간을 둘것인지?
        //아니면 모든 리스트를 제한시간안에 다 풀것인지?
        //아니면 두개 다 둘것인지
        //고민해보기 일단 입력마다 시간제한 두기

        //제한시간
        public float Timer { get; set; }
        public Stopwatch watch = new Stopwatch();

        //문제집
        Queue<ArrowData.EType> queue = new Queue<ArrowData.EType>();
        //답지
        public int Answer{ get; set; }

        public ArrowGame(int level)
        {
            Level = level;
            Score = 0;
            Timer = 0f;
        }

        public void Init()
        {
            watch.Start();

            Timer = 0f;
            Score = 0;
            Level = 1;

            //초반 게임 세팅
            //추후 도감작에 따라 초반 레벨(난이도) 변경해야함
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                queue.Enqueue((ArrowData.EType)rnd.Next(0, 3));
            }
        }


        //레벨당 몇개의 화살표를 뱉을건지?
        //한줄당 최대 8개가 나오도록
        public void ChangeLevel()
        {
            
        }

        public Queue<ArrowData.EType> GetList()
        {
            return queue;
        }

        public bool CheckInputArrow(ConsoleKey input)
        {
            bool result = false;
            result = (int)queue.Peek() == input - ConsoleKey.LeftArrow ? true : false;
            if(result){
                queue.Dequeue();
                Answer++;
                return result;
            }

            return result;
        }

    }
}
