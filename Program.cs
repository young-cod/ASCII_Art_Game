using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class Program
    {
        static Stopwatch watch = new Stopwatch(); // 프레임 제어용 스톱워치
        //윈도우 최대 너비 Console.LargestWindowWidth(32);
        //윈도우 최대 높이 Console.LargestWindowHeight(86)
        const int WIN_MAX_WIDTH = 200;
        const int WIN_MAX_HEIGHT = 60;


        static void SetConsole(int width, int height, bool isCursor)
        {

            //1. 버퍼 사이즈 먼저 정해주기! 중요!
            //실제 텍스트들이 담길 사이즈들
            Console.SetBufferSize(width, height);

            //2. 윈도우 크기 정해주기
            //-> 버퍼 사이즈 보다 커질 수 없음
            Console.SetWindowSize(width, height);

            //커서 표시 유무
            Console.CursorVisible = isCursor;

            //콘솔 인코딩 변경(아스키코드를위해)
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }

        static void Main(string[] args)
        {
            SetConsole(WIN_MAX_WIDTH, WIN_MAX_HEIGHT, false);

            Start();  // 초기화 함수 호출
            while (true)
            {
                Update(); // 무한 반복으로 게임 루프 실행
            }
        }

        // 초기화 함수 (Unity의 Start와 유사)
        static void Start()
        {
            watch.Start();
            GameManager.Instance.Init();
        }

        // 반복 실행 함수 (Unity의 Update와 유사)
        static void Update()
        {
            InputManager.Instance.ProcessInput();
            SceneManager.Instance.Render();
            //if (watch.ElapsedMilliseconds >= 1000) // 0.1초마다 실행
            //{
            //    watch.Restart(); // 시간 초기화
            //    Render();        // 화면 렌더링
            //}
        }

        // 키 입력 처리
        static void HandleInput()
        {

        }


        // 화면 렌더링
        static void Render()
        {
            SceneManager.Instance.ClearScreenByFilling();

            SceneManager.Instance.Render();
        }


    }
}