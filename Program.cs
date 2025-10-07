namespace AsciiArt
{
    internal class Program
    {
     
        static void Main(string[] args)
        {
            
            Start();  // 초기화 함수 호출

            while (true)
            {
                Update(); // 무한 반복으로 게임 루프 실행
            }
        }

        // 초기화 함수 (Unity의 Start와 유사)
        static void Start()
        {
            GameManager.Instance.Init();
           
        }

        // 반복 실행 함수 (Unity의 Update와 유사)
        static void Update()
        {

            InputManager.Instance.ProcessInput();
            SceneManager.Instance.Render();
            SceneManager.Instance.Update();

            //Render();
        }
    }
}