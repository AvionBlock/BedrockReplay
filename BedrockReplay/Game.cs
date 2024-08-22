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

        public async Task Run()
        {
            var window = WindowManager.CreateWindow(WindowOptions.Default);
            window.OnWindowLoad += Load;
            _ = Task.Run(() => 
            {
                try
                {
                    window.Window.Run();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });

            await window.WaitForInitialization();

            var window2 = WindowManager.CreateWindow(WindowOptions.Default);
            window2.OnWindowLoad += Load;
            _ = Task.Run(() =>
                {
                    try
                    {
                        window2.Window.Run();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            );
            await WindowManager.BlockingOpenWindows();
        }

        private void Load(WindowInstance window)
        {
            window.SetOpenGL();
            window.Engine.SetClearColor(Color.Aqua);
        }
    }
}
