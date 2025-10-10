using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    class SuccessScene :Scene
    {
        
        readonly float returnTimer = 5f;
        float timer;

        string[] successArt;

        public SuccessScene(string[] art){
            successArt = art;
        }

        public override void Init()
        {
            Type = EType.Success;

            GameManager.Instance.watch.Restart();
            //GameManager.Instance.State = GameManager.EGameState.Pause;
            //SceneManager.Instance.LoadScene(EType.Main);
        }

        public override void Render()
        {
            int centerX = ScreenBuffer.currentW / 2;
            ScreenBuffer.Draw(centerX - (successArt.Max(line => line.Length) / 2), 2, successArt, ConsoleColor.Green);

            //ScreenBuffer.Draw(centerX -  9, successArt.Length + 12, $"Main screen after {timer:0.0} seconds.");

            //ScreenBuffer.Draw(centerX - (gameOver.Length) - 9, skelton.Length + 15, "Please Enter Any Key!");
        }

        public override void Update()
        {
            timer = returnTimer - GameManager.Instance.watch.ElapsedMilliseconds / 1000f;
            if (timer < 0) SceneManager.Instance.LoadScene(EType.Main);
        }
    }
}
