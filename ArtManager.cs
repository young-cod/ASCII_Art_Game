using System;
using System.Collections.Generic;
using System.Linq;

namespace AsciiArt
{

    partial class ArtManager : Singleton<ArtManager>
    {
        string[] currentArt;

        char[][] blurArtChars;
        public string[] BlurArt
        {
            get
            {
                return blurArtChars.Select(line => new string(line)).ToArray();
            }
        }

        public float Timer { get; set; } = 2000f;

        public string[] ArtToBlur(string[] art)
        {
            // 원본 데이터 저장
            currentArt = art;

            // char 배열의 배열을 초기화합니다.
            blurArtChars = new char[art.Length][];

            // 블러 처리된 char 배열을 생성하고 저장합니다.
            for (int i = 0; i < art.Length; i++)
            {
                // 현재 줄의 길이
                int lineLength = art[i].Length;
                blurArtChars[i] = new char[lineLength];

                for (int j = 0; j < lineLength; j++)
                {
                    blurArtChars[i][j] = '@';
                }
            }
            return blurArtChars.Select(line=>new string(line)).ToArray();
        }


        public void BlurToArt()
        {
            List<Tuple<int, int>> allCoordinates = new List<Tuple<int, int>>();

            // 원본 아트(currentArt)에서 공백이 아닌 모든 문자의 좌표를 리스트에 추가.
            for (int y = 0; y < currentArt.Length; y++)
            {
                for (int x = 0; x < currentArt[y].Length; x++)
                {
                    if (currentArt[y][x] != ' ')
                    {
                        allCoordinates.Add(new Tuple<int, int>(y, x));
                    }
                }
            }

            Random rnd = new Random();
            int n = allCoordinates.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Tuple<int, int> value = allCoordinates[k];
                allCoordinates[k] = allCoordinates[n];
                allCoordinates[n] = value;
            }

            int restorationCount = Math.Min(250, allCoordinates.Count);

            for (int i = 0; i < restorationCount; i++)
            {
                int rndY = allCoordinates[i].Item1;
                int rndX = allCoordinates[i].Item2;

                if (rndY < blurArtChars.Length && rndX < blurArtChars[rndY].Length)
                {
                    blurArtChars[rndY][rndX] = currentArt[rndY][rndX];
                }
            }
        }

    }
}
