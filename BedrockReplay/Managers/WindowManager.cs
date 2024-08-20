using AvionEngine.Interfaces;
using Silk.NET.Windowing;
using System.Drawing;

namespace BedrockReplay.Managers
{
    public class WindowManager
    {
        public List<IEngine> Windows;

        public WindowManager()
        {
            Windows = new List<IEngine>();
        }

        public void BlockingOpenWindows()
        {
            while(Windows.Count > 0)
            {
                Task.Delay(1);
            }
        }

        public async Task<IEngine> CreateOpenGLWindow()
        {
            var windowOptions = WindowOptions.Default;
            var window = Window.Create(windowOptions);
            IEngine? engine = null;

            window.Load += WindowLoad;
            _ = Task.Run(window.Run);

            while (engine == null)
            {
                await Task.Delay(1); //1ms delay so we don't burn the CPU.
            }

            return engine;

            void WindowLoad()
            {
                var renderer = new AvionEngine.OpenGL.Renderer(window);
                engine = new AvionEngine.AvionEngine(renderer);
                renderer.SetClearColor(Color.Aqua);
                Windows.Add(engine);
                window.Load -= WindowLoad;
                window.Closing += WindowClose;

                void WindowClose()
                {
                    Windows.Remove(engine);
                    window.Closing -= WindowClose;
                }
            }
        }
    }
}
