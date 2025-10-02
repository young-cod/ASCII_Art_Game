using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsciiArt
{
    static class Tools
    {

        public static int GetCenterPosX(string str)
        {
            int window_width = Console.WindowWidth;

            return (window_width / 2) - (str.Length / 2);
        }

        public static void WriteLineAt(int x, int y, string str = "")
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);
        }
        public static void WriteAt(int x, int y, string str = "")
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }

        /// <summary>
        /// (x,y)위치에 아트디자인 출력
        /// </summary>
        /// <param name="x">x축</param>
        /// <param name="y">y축</param>
        /// <param name="artLine"></param>
        public static void ArtLineAllRenderAt(int x, int y, string[] artLine)
        {
            int artHeight = artLine.Length;
            for (int i = 0; i < artHeight; i++)
            {
                //Console.SetCursorPosition(x, i + y);
                //Console.WriteLine(artLine[i]);

                WriteLineAt(x, i + y, artLine[i]);
            }
        }
    }
    static class Debug
    {
        public static void Log(Object value, int timer=0)
        {
            Console.WriteLine(value);
            Thread.Sleep(timer*1000);
        }

    }
}
