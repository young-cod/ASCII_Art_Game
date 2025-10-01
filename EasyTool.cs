using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
        
        public static void WriteLineAt(int x, int y, string str = ""){
            Console.SetCursorPosition(x, y);
            Console.WriteLine(str);
        }
        public static void WriteAt(int x, int y, string str = "")
        {
            Console.SetCursorPosition(x, y);
            Console.Write(str);
        }
    }

}
