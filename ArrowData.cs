namespace AsciiArt
{
    internal static class ArrowData
    {
        public enum EType
        {
            Left, Up,Right, Down,Check
        }

        static readonly string[] LeftArrow = new string[]{
            "  __     ",
            " / /____ ",
            "/ /_____|",
            "\\ \\_____|",
            " \\_\\     "
        };
        static readonly string[] UpArrow = new string[] {
            "  ____  ",
            " / /\\ \\ ",
            "/_/ _\\_\\",
            "  || |  ",
            "  ||_|  "
        };
        static readonly string[] RightArrow = new string[] {
            "     __  ",
            " ____\\ \\ ",
            "|_____\\ \\",
            "|_____/ /",
            "     /_/ "
        };
        
        static readonly string[] DownArrow = new string[] {
            "   _    ",
            "  | ||  ",
            " _|_||_ ",
            "\\ \\  / /",
            " \\_\\/_/ "
        };

        //체크 표시
        public static readonly string[] Success = new string[]{
            "      __  ",
            "__   / /  ",
            "\\ \\ / /  ",
            " \\ V /   ",
            "  \\_/    "
        };

        public static readonly string[] Fail = new string[]{
            "__  __",
            "\\ \\/ /",
            " \\  / ",
            " /  \\ ",
            "/_/\\_\\"
        };

        public static string[] GetArrowData(ArrowData.EType type)
        {
            switch (type)
            {
                case EType.Left: return LeftArrow;
                case EType.Up: return UpArrow;
                case EType.Right: return RightArrow;
                case EType.Down: return DownArrow;
            }

            return null;
        }
    }
}
