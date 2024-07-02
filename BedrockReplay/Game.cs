using BedrockReplay.Core.Rendering;
using BedrockReplay.OpenGL;
using BedrockReplay.OpenGL.Rendering;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace SharpVE
{
    public class Game
    {
        private IWindow window;
        private Renderer? renderer;
        
        public Game()
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            window = Window.Create(options);
            window.Load += OnLoad;

            window.Run();
        }

        private void OnLoad()
        {
            renderer = new Renderer(window);

            renderer.AddShader(renderer.CreateShader(Shader.basicVertexCode, Shader.basicFragmentCode));
        }
    }
}
