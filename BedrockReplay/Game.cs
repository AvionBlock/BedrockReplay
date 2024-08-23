using BedrockReplay.Managers;
using BedrockReplay.Shaders;
using Silk.NET.Windowing;
using System.Drawing;

namespace SharpVE
{
    public class Game
    {
        public static Arch.Core.World ECSWorld = Arch.Core.World.Create();
        public Game()
        {
        }

        public async Task Run()
        {
            var window = WindowManager.CreateWindow(WindowOptions.Default);
            window.OnWindowLoad += Load;
            window.Window.Run();
        }

        private void Load(WindowInstance window)
        {
            window.SetOpenGL();
            window.Engine.SetClearColor(Color.Aqua);

            new Shader(window.Engine, "./Shaders/Default.vert", "./Shaders/Default.frag");
        }
    }
}
