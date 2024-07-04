using BedrockReplay.Core;
using BedrockReplay.Core.Rendering;
using System.Numerics;

namespace BedrockReplay
{
    public class ProjectionShader : Shader
    {
        private Shader shader;
        private Camera camera;

        public ProjectionShader(Shader shader, Camera camera)
        {
            this.shader = shader;
            this.camera = camera;
        }

        public override void Bind()
        {
            shader.Bind();
            var model = Matrix4x4.Identity; //Caching is faster than storing in a property.
            var view = camera.GetViewMatrix();
            var projection = camera.ProjectionMatrix;

            shader.SetUniform4(nameof(model), model);
            shader.SetUniform4(nameof(view), view);
            shader.SetUniform4(nameof(projection), projection);
        }
    }
}
