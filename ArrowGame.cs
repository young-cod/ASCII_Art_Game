using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsciiArt
{
    internal class ArrowGame
    {
        //초기 생성 개수
        const int CREATECOUNT = 16;
        public const int LINE_MAXCOUNT = 8;

        //todo : 레벨, 난이도, 점수 세팅하기
        public int Level { get; private set; }

        //<레벨,<화살표개수, 타이머>>
        public Dictionary<int, Tuple<int, float>> dicLevelCount = new Dictionary<int, Tuple<int, float>>()
        {
            // 초급 (1-10)
            { 1, new Tuple<int, float>(3, 7.0f) },
            { 2, new Tuple<int, float>(4, 6.5f) },
            { 3, new Tuple<int, float>(4, 6.0f) },
            { 4, new Tuple<int, float>(5, 6.0f) },
            { 5, new Tuple<int, float>(5, 5.5f) },
            { 6, new Tuple<int, float>(6, 5.5f) },
            { 7, new Tuple<int, float>(6, 5.0f) },
            { 8, new Tuple<int, float>(7, 5.0f) },
            { 9, new Tuple<int, float>(7, 4.5f) },
            { 10, new Tuple<int, float>(8, 4.0f) },

            // 중급 (11-20)
            { 11, new Tuple<int, float>(9, 4.0f) },
            { 12, new Tuple<int, float>(10, 4.0f) },
            { 13, new Tuple<int, float>(11, 3.8f) },
            { 14, new Tuple<int, float>(12, 3.8f) },
            { 15, new Tuple<int, float>(13, 3.5f) },
            { 16, new Tuple<int, float>(14, 3.5f) },
            { 17, new Tuple<int, float>(15, 3.2f) },
            { 18, new Tuple<int, float>(16, 3.2f) },
            { 19, new Tuple<int, float>(17, 3.0f) },
            { 20, new Tuple<int, float>(18, 3.0f) },

            // 고급 (21-30)
            { 21, new Tuple<int, float>(20, 2.8f) },
            { 22, new Tuple<int, float>(22, 2.8f) },
            { 23, new Tuple<int, float>(24, 2.6f) },
            { 24, new Tuple<int, float>(26, 2.6f) },
            { 25, new Tuple<int, float>(28, 2.4f) },
            { 26, new Tuple<int, float>(30, 2.4f) },
            { 27, new Tuple<int, float>(32, 2.2f) },
            { 28, new Tuple<int, float>(34, 2.2f) },
            { 29, new Tuple<int, float>(36, 2.0f) },
            { 30, new Tuple<int, float>(38, 2.0f) },

            // 최상급 (31-40)
            { 31, new Tuple<int, float>(38, 1.8f) },
            { 32, new Tuple<int, float>(38, 1.7f) },
            { 33, new Tuple<int, float>(39, 1.7f) },
            { 34, new Tuple<int, float>(39, 1.6f) },
            { 35, new Tuple<int, float>(40, 1.6f) },
            { 36, new Tuple<int, float>(40, 1.5f) },
            { 37, new Tuple<int, float>(40, 1.4f) },
            { 38, new Tuple<int, float>(40, 1.3f) },
            { 39, new Tuple<int, float>(40, 1.2f) },
            { 40, new Tuple<int, float>(40, 1.1f) }
        };
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
        public int Answer { get; set; }

        public ArrowGame(int level)
        {
            Level = level;
            Score = 0;
            Timer = dicLevelCount[level].Item2;
        }

        public void Init()
        {
            watch.Start();

            //초반 게임 세팅
            //추후 도감작에 따라 초반 레벨(난이도) 변경해야함
            SetLevel(1);
        }


        //레벨당 몇개의 화살표를 뱉을건지?
        //한줄당 최대 8개가 나오도록
        public void SetLevel(int lvl)
        {
            Level = lvl;

            int arrowCnt = dicLevelCount[Level].Item1;
            Timer = dicLevelCount[Level].Item2;
            Answer = 0;
            watch.Restart();

            Random rnd = new Random();

            for (int i = 0; i < arrowCnt; i++)
            {
                //레프트, 업, 라이트, 다운
                queue.Enqueue((ArrowData.EType)rnd.Next(0, 4));
            }

            GameManager.Instance.SetArrowList(queue);
        }

        public int CalcArrowScore(int currentLevel)
        {
            float timeRemaining = Timer - watch.ElapsedMilliseconds / 1000f;
            // 시간 내에 맞추지 못했거나 (타이머가 0 이하), 너무 오래 걸렸으면 보너스는 0
            if (timeRemaining <= 0)
            {
                return 10; // 기본 점수만 지급
            }

            // 보너스 배율 결정
            float bonusMultiplier;

            if (currentLevel <= 10) bonusMultiplier = 1;
            else if (currentLevel <= 20) bonusMultiplier = 1.5f;
            else if (currentLevel <= 30) bonusMultiplier = 2;
            else bonusMultiplier = 3; // 31~40 레벨

            // 점수 계산
            int baseScore = 10;
            int bonusScore = (int)(timeRemaining * bonusMultiplier);

            return baseScore + bonusScore;
        }

        public Queue<ArrowData.EType> GetList()
        {
            return queue;
        }


        //들어온 키값이 문제집이랑 같은지 검사
        public bool CheckInputArrow(ConsoleKey input)
        {
            if (queue.Count <= 0) return false;

            bool result = false;
            int typeNum = input - ConsoleKey.LeftArrow;
            Console.SetCursorPosition(0, 5);
            //Debug.Log($"{(int)queue.Peek()},{typeNum}");
            result = (int)queue.Peek() == input - ConsoleKey.LeftArrow ? true : false;
            if (result)
            {
                queue.Dequeue();
                Answer++;
                watch.Restart();
                Timer = dicLevelCount[Level].Item2;
                Score += CalcArrowScore(Level);

                //다 풀었다면 레벨업
                if (Answer == GameManager.Instance.arrowList.Count)
                {
                    //ArtManager.Instance.Timer -= Level * 100;
                    //if (ArtManager.Instance.Timer <= 1000) ArtManager.Instance.Timer = 1000;
                    ArtManager.Instance.BlurToArt();
                    SetLevel(Level + 1);
                }

                return result;
            }

            return result;
        }

    }
}
