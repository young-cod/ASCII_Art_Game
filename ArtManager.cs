using System;
using System.Collections.Generic;
using System.Linq;

namespace AsciiArt
{

    partial class ArtManager : Singleton<ArtManager>
    {
        public string[] currentArt;

        char[][] blurArtChars;
        
        private List<Tuple<int, int>> allCoordinates =new List<Tuple<int, int>>();

        public string[] BlurArt
        {
            get
            {
                return blurArtChars.Select(line => new string(line)).ToArray();
            }
        }

        public float Timer { get; set; } = 2000f;
        //블러아트가 선명하게 바뀌는 최소 픽셀단위
        int PixelMinCount;
        public bool isClear = false;

        public string[] ArtToBlur(string[] art)
        {
            // 원본 데이터 저장
            currentArt = art;

            // char 배열의 배열을 초기화
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
            PixelMinCount = (currentArt.Length * currentArt.Max(line => line.Length)) / 5;
            //공백이 아닌 모든 문자의 좌표를 리스트에 추가
            for (int y = 0; y < currentArt.Length; y++)
            {
                for (int x = 0; x < currentArt[y].Length; x++)
                {
                    //if (currentArt[y][x] != ' ')
                    //{
                    //    allCoordinates.Add(new Tuple<int, int>(y, x));
                    //}
                    allCoordinates.Add(new Tuple<int, int>(y, x));
                }
            }

            Shuffle();

            return BlurArt;
        }


        public void BlurToArt()
        {
            int restorationCount = Math.Min(PixelMinCount, allCoordinates.Count);

            // 복원할 픽셀이 없거나 1 미만이면 성공
            if (allCoordinates.Count == 0)
            {
                isClear = true;
                return;
            }

            if (restorationCount < 1)
            {
                // 남은 픽셀이 있다면 모두 복원
                restorationCount = allCoordinates.Count;
            }


            for (int i = 0; i < restorationCount; i++)
            {
                Tuple<int, int> coord = allCoordinates[0];
                int rndY = coord.Item1;
                int rndX = coord.Item2;

                // 안전을 위해 경계 검사
                if (rndY < blurArtChars.Length && rndX < blurArtChars[rndY].Length)
                {
                    blurArtChars[rndY][rndX] = currentArt[rndY][rndX];
                }

                // 복원된 좌표는 리스트에서 제거. 
                allCoordinates.RemoveAt(0);
            }

            // 루프 후, 모든 좌표가 복원되었는지 확인
            if (allCoordinates.Count == 0)
            {
                isClear = true;
            }
        }

        void Shuffle()
        {
            //좌표 랜덤으로 섞기
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
        }
    }
}
