using BedrockReplay.Managers;

namespace SharpVE
{
    public class Game
    {
        static WindowManager WindowManager = new WindowManager();

        public Game()
        {
        }

        public async Task Run()
        {
            await WindowManager.CreateOpenGLWindow();
            await WindowManager.CreateOpenGLWindow();
            WindowManager.BlockingOpenWindows();
        }
    }
}
