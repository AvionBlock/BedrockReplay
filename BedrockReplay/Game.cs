using BedrockReplay.Core.Rendering;
using BedrockReplay.OpenGL;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace SharpVE
{
    public class Game
    {
        private Renderer renderer;
        
        public Game()
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            renderer = new Renderer(options);

            renderer.window.Load += OnLoad;

            renderer.Run();
        }

        private void OnLoad()
        {
            renderer.AddShader(new Shader());
            renderer.RenderMesh(new Mesh());
        }
    }
}
