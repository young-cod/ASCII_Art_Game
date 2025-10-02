using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal static class ArrowData
    {
        public enum EType
        {
            Left, Up,Right, Down,Check
        }

        public static readonly string[] LeftArrow = new string[]{
            "  __     ",
            " / /____ ",
            "/ /_____|",
            "\\ \\_____|",
            " \\_\\     "
        };
        public static readonly string[] UpArrow = new string[] {
            "  ____  ",
            " / /\\ \\ ",
            "/_/ _\\_\\",
            "  || |  ",
            "  ||_|  "
        };
        public static readonly string[] RightArrow = new string[] {
            "     __  ",
            " ____\\ \\ ",
            "|_____\\ \\",
            "|_____/ /",
            "     /_/ "
        };
        
        public static readonly string[] DownArrow = new string[] {
            "   _    ",
            "  | ||  ",
            " _|_||_ ",
            "\\ \\  / /",
            " \\_\\/_/ "
        };

        //체크 표시
        public static readonly string[] Check = new string[]{
            "      __",
            "__   / /",
            "\\ \\ / /",
            " \\ V / ",
            "  \\_/  "
        };
    }
}
