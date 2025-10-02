using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    interface IInputHandler{
        void HandleInput(ConsoleKeyInfo keyInfo);
    }

    internal class InputManager : Singleton<InputManager>,IInputHandler
    {
        public IInputHandler currentHandler;

        public void SetHandler(IInputHandler handler){
            this.currentHandler = handler;
        }  

        public void ProcessInput(){
            if(currentHandler == null ){
                return;
            }

            //키 입력이 있으면
            if(Console.KeyAvailable){

                ConsoleKeyInfo keyInfo = Console.ReadKey(false);
                currentHandler.HandleInput(keyInfo);
            }
        }


        public void HandleInput(ConsoleKeyInfo keyInfo)
        {
            throw new NotImplementedException();
        }

    }
}
