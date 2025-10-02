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
     
        static void Main(string[] args)
        {
            
            Start();  // 초기화 함수 호출

            //while (true)
            //{
            //    // ----------------------------------------------------
            //    // 1. 입력 처리 및 로직 업데이트 (Update Phase)
            //    // ----------------------------------------------------
            //    // (예: 키 입력에 따라 playerX, playerY 변경)

            //    //playerX++; // 임시로 이동 로직을 추가
            //    //if (playerX >= 90) playerX = 10;

            //    // ----------------------------------------------------
            //    // 2. 그리기 (Draw Phase)
            //    // ----------------------------------------------------

            //    // A. 배경이나 정적 요소 그리기 (예: MapArt.Data.ArtLines)
            //    // ScreenBuffer.Draw(MapArt.Data.ArtLines, 0, 0);

            //    // B. 동적 요소 그리기 (플레이어 아스키 아트)
            //    ScreenBuffer.Draw(playerArt.ArtLine, playerX, playerY);

            //    // ----------------------------------------------------
            //    // 3. 화면 갱신 (Flip Phase)
            //    // ----------------------------------------------------
            //    ScreenBuffer.Flip(); // 💡 떨림 없이 화면을 갱신!

            //    // 프레임 속도 조절
            //    System.Threading.Thread.Sleep(50); // 20 FPS (1000ms / 50ms)
            //}

            while (true)
            {
                Update(); // 무한 반복으로 게임 루프 실행
            }
        }

        // 초기화 함수 (Unity의 Start와 유사)
        static void Start()
        {
            GameManager.Instance.Init();
           
        }

        // 반복 실행 함수 (Unity의 Update와 유사)
        static void Update()
        {

            InputManager.Instance.ProcessInput();
            SceneManager.Instance.Render();

            //Render();
        }

       
        // 화면 렌더링
        static void Render()
        {

            //SceneManager.Instance.ClearScreenByFilling();

            SceneManager.Instance.Render();
        }


    }
}