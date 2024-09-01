using BedrockReplay.Managers;
using Silk.NET.Windowing;
using AvionEngine.Graphics;

namespace BedrockReplay
{
    public class Game
    {
        private AVBuffer? VertexBuffer;
        public async Task Run()
        {
            var window = WindowManager.CreateWindow(WindowOptions.Default);
            window.OnWindowLoad += WindowLoad;
            _ = Task.Run(window.Window.Run);
            await WindowManager.BlockingOpenWindows();
        }

        private void WindowLoad(WindowInstance window)
        {
            window.SetOpenGL();
        }
    }
}
