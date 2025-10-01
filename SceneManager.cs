using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    internal class SceneManager : Singleton<SceneManager>
    {
        public Scene CurrentScene { get; set; }
        Scene.ETYPE currentState = Scene.ETYPE.Main;

        public override void Init()
        {
            MainScene main = new MainScene();
            CurrentScene = main;
            currentState = Scene.ETYPE.Main;
            CurrentScene.Init();
        }

        public void LoadScene(Scene.ETYPE scene)
        {
            //이전 씬 정리
            InputManager.Instance.SetHandler(null);


            //현재 씬 변경
            CurrentScene = GetScene(scene);

            //씬 initiailize
            CurrentScene.Init();
        }

        private Scene GetScene(Scene.ETYPE type){
            switch (type)
            {
                case Scene.ETYPE.Main:return new MainScene();
                case Scene.ETYPE.Game:return new GameScene();
                case Scene.ETYPE.ArtBook:
                case Scene.ETYPE.DetailArtBook:
                case Scene.ETYPE.Achievement:
                default:
                    return null;
            }
        }

       

        public void Render()
        {
            //ClearScreenByFilling();

            CurrentScene.Render();
        }
        // 화면 지우기 (빈 문자로 채우기)
        public void ClearScreenByFilling()
        {
            for (int i = 0; i < Console.WindowHeight; i++) // 세로 한 줄씩
            {
                Console.SetCursorPosition(0, i);                       // 줄 시작 위치
                Console.Write(new string(' ', Console.WindowWidth));   // 공백으로 채우기
            }
            Console.SetCursorPosition(0, 0); // 커서 원점으로 이동
        }
    }
}
