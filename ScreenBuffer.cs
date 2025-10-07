using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    struct ColorChar
    {
        public char Character;
        public ConsoleColor ForegroundColor;
        public ConsoleColor BackgroundColor;

        public ColorChar(char c, ConsoleColor fColor, ConsoleColor bColor)
        {
            Character = c;
            ForegroundColor = fColor;
            BackgroundColor = bColor;
        }

        public static bool operator ==(ColorChar a, ColorChar b)
        {
            return a.Character == b.Character &&
                   a.ForegroundColor == b.ForegroundColor &&
                   a.BackgroundColor == b.BackgroundColor;
        }

        public static bool operator !=(ColorChar a, ColorChar b)
        {
            return !(a == b);
        }


    }
    internal static class ScreenBuffer
    {
        //윈도우 최대 너비 Console.LargestWindowWidth(320);
        //윈도우 최대 높이 Console.LargestWindowHEIGHT(86)
        const int WIN_MAX_WIDTH = 200;
        const int WIN_MAX_HEIGHT = 60;

        //실제 os가 허용하는 사이즈
        public static int currentW;
        public static int currentH;

        //이전 프레임의 내용을 저장하는 버퍼
        private static ColorChar[,] PreviousBuffer = new ColorChar[WIN_MAX_WIDTH, WIN_MAX_HEIGHT];
        //현재 프레임의 내용을 저장할 버퍼
        private static ColorChar[,] CurrentBuffer = new ColorChar[WIN_MAX_WIDTH, WIN_MAX_HEIGHT];

        // 기본 색상
        private static ConsoleColor defaultForeground = ConsoleColor.White;
        private static ConsoleColor defaultBackground = ConsoleColor.Black;

        // 버퍼 초기화
        public static void Init()
        {
            //실제 os가 허용하는 사이즈
            currentW = Math.Min(WIN_MAX_WIDTH, Console.LargestWindowWidth);
            currentH = Math.Min(WIN_MAX_HEIGHT, Console.LargestWindowHeight);

            ColorChar emptyChar = new ColorChar(' ', defaultForeground, defaultBackground);

            //Console.WriteLine($"{currentH},{currentW}");

            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    PreviousBuffer[x, y] = emptyChar;
                    CurrentBuffer[x, y] = emptyChar;
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
        public static void Draw(int startX, int startY, string[] artLines,
        ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            for (int y = 0; y < artLines.Length && startY + y < currentH; y++)
            {
                for (int x = 0; x < artLines[y].Length && startX + x < currentW; x++)
                {
                    // 현재 버퍼에 문자 데이터를 기록
                    CurrentBuffer[startX + x, startY + y] = new ColorChar(
                    artLines[y][x], foreColor, backColor
                    );
                }
            }
        }

        //특정 위치에 문자열 쓰기
        public static void Draw(int startX, int startY, string artLines,
        ConsoleColor foreColor = ConsoleColor.White, ConsoleColor backColor = ConsoleColor.Black)
        {
            for (int x = 0; x < artLines.Length && startX + x < currentW; x++)
            {
                // 현재 버퍼에 문자 데이터를 기록
                CurrentBuffer[startX + x, startY] = new ColorChar(
                    artLines[x], foreColor, backColor
                );
            }
        }

        // 💡 버퍼를 실제 콘솔 화면에 출력하고 버퍼를 스왑합니다. (이중 버퍼링 로직)
        public static void Flip()
        {
            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    if (CurrentBuffer[x, y] != PreviousBuffer[x, y])
                    {
                        var cell = CurrentBuffer[x, y];

                        // 색상 변경
                        Console.ForegroundColor = cell.ForegroundColor;
                        Console.BackgroundColor = cell.BackgroundColor;

                        // 커서 이동 및 문자 출력
                        Console.SetCursorPosition(x, y);
                        Console.Write(cell.Character);
                    }
                }
            }

            // 색상 초기화
            Console.ForegroundColor = defaultForeground;
            Console.BackgroundColor = defaultBackground;

            // 버퍼 스왑
            var temp = PreviousBuffer;
            PreviousBuffer = CurrentBuffer;
            CurrentBuffer = temp;

            // 현재 버퍼 초기화
            var emptyChar = new ColorChar(' ', defaultForeground, defaultBackground);
            for (int y = 0; y < currentH; y++)
            {
                for (int x = 0; x < currentW; x++)
                {
                    CurrentBuffer[x, y] = emptyChar;
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
