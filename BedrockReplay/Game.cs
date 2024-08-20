using AvionEngine.Interfaces;
using Silk.NET.Windowing;
using System.Drawing;

namespace SharpVE
{
    public class Game
    {
        static IEngine Engine { get; set; }

        public Game()
        {
            var windowOptions = WindowOptions.Default;
            var window = Window.Create(windowOptions);

            window.Load += WindowLoad;
            window.Run();

            void WindowLoad()
            {
                var renderer = new AvionEngine.OpenGL.Renderer(window);
                Engine = new AvionEngine.AvionEngine(renderer);
                renderer.SetClearColor(Color.Aqua);
            }
        }
    }
}
