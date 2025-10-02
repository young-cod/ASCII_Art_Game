using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal static class ScreenBuffer
    {
        //윈도우 최대 너비 Console.LargestWindowWidth(320);
        //윈도우 최대 높이 Console.LargestWindowHEIGHT(86)
        public const int WIN_MAX_WIDTH = 200;
        public const int WIN_MAX_HEIGHT = 60;

        //실제 os가 허용하는 사이즈
        static int currentW;
        static int currentH;

        //이전 프레임의 내용을 저장하는 버퍼
        private static char[,] PreviousBuffer = new char[WIN_MAX_WIDTH, WIN_MAX_HEIGHT];
        //현재 프레임의 내용을 저장할 버퍼
        private static char[,] CurrentBuffer = new char[WIN_MAX_WIDTH, WIN_MAX_HEIGHT];

        // 버퍼 초기화
        public static void Init()
        {
            //실제 os가 허용하는 사이즈
            currentW = Math.Min(WIN_MAX_WIDTH, Console.LargestWindowWidth);
            currentH = Math.Min(WIN_MAX_HEIGHT, Console.LargestWindowHeight);

            //Console.WriteLine($"{currentH},{currentW}");

            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    PreviousBuffer[x, y] = ' ';
                    CurrentBuffer[x, y] = ' ';
                }
            }

            //1. 버퍼 사이즈 먼저 정해주기! 중요!
            //실제 텍스트들이 담길 사이즈들
            Console.SetBufferSize(currentW, currentH);

            //2. 윈도우 크기 정해주기
            //-> 버퍼 사이즈 보다 커질 수 없음
            Console.SetWindowSize(currentW, currentH);

            //커서 표시 유무
            Console.CursorVisible = false;

            //콘솔 인코딩 변경(아스키코드를위해)
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
        }

        // 특정 위치에 아스키 아트 데이터를 쓰기
        public static void Draw(int startX, int startY, string[] artLines)
        {
            for (int y = 0; y < artLines.Length && startY + y < currentH; y++)
            {
                for (int x = 0; x < artLines[y].Length && startX + x < currentW; x++)
                {
                    // 현재 버퍼에 문자 데이터를 기록
                    CurrentBuffer[startX + x, startY + y] = artLines[y][x];
                }
            }
        }

        //특정 위치에 문자열 쓰기
        public static void Draw(int startX, int startY, string artLines)
        {
            for (int x = 0; x < artLines.Length && startX + x < currentW; x++)
            {
                // 현재 버퍼에 문자 데이터를 기록
                CurrentBuffer[startX + x, startY] = artLines[x];
            }
        }

        // 💡 버퍼를 실제 콘솔 화면에 출력하고 버퍼를 스왑합니다. (이중 버퍼링 로직)
        public static void Flip()
        {
            // 이전 버퍼와 현재 버퍼를 비교하여 변경된 부분만 콘솔에 출력
            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    if (CurrentBuffer[x, y] != PreviousBuffer[x, y])
                    {
                        // 1. 커서를 변경된 위치로 이동
                        Console.SetCursorPosition(x, y);

                        // 2. 변경된 문자 하나만 출력
                        Console.Write(CurrentBuffer[x, y]);
                    }
                }
            }

            // 3. 버퍼 스왑: 현재 버퍼의 내용을 다음 프레임의 이전 버퍼로 복사 (매우 중요!)
            // Array.Copy 등을 사용할 수도 있지만, 복사를 피하고 배열 자체를 교체하는 것이 더 빠릅니다.
            var temp = PreviousBuffer;
            PreviousBuffer = CurrentBuffer;
            CurrentBuffer = temp;

            // 다음 프레임을 위해 현재 버퍼(새로운 CurrentBuffer)를 초기화 (공백으로 지우기)
            // Draw 호출 시 덮어쓰지 않는 영역에 잔상이 남지 않도록 합니다.
            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    CurrentBuffer[x, y] = ' ';
                }
            }
        }

        //public static void Clear(){
        //    for (int y = 0; y < currentH; y++)
        //    {
        //        for (int x = 0; x < currentW; x++)
        //        {
        //            CurrentBuffer[x, y] = ' ';
        //        }
        //    }

        //    Array.Copy(CurrentBuffer, PreviousBuffer, currentW * currentH);

        //    // 안 지워질 시
        //    // Console.Clear(); 
        //}
    }
}
