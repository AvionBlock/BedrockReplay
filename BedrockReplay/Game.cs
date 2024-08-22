using BedrockReplay.Managers;
using Silk.NET.Windowing;
using System.Drawing;

namespace SharpVE
{
    public class Game
    {
        public Game()
        {
        }

        public void Run()
        {
            var window = WindowManager.CreateWindow(WindowOptions.Default);
            window.OnWindowLoad += Load;
            _ = Task.Run(window.Window.Run);
            WindowManager.BlockingOpenWindows();
        }

        private static void Load(WindowInstance window)
        {
            window.SetOpenGL();
            window.Engine.SetClearColor(Color.Aqua);
        }
    }
}
