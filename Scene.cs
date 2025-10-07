using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArt
{
    abstract class Scene
    {
        //씬 타입
        public enum EType { Main, Game, ArtBook, DetailArtBook, Achievement,GameOver }
        public EType Type { get; protected set; }


       
        /// <summary>
        /// 1. 씬 초기화하기(clear) 
        /// 2.씬 타입(Scene.EType)이 뭔지 정하기
        /// 3. 인풋핸들러 세팅해주기</summary>
        abstract public void Init();
        abstract public void Render();

        virtual public void Update() { }

    }
}
