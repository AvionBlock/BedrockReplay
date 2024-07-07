using BedrockReplay.Core;
using BedrockReplay.Core.Interfaces;
using BedrockReplay.Core.Rendering;
using System.Numerics;

namespace BedrockReplay
{
    public class ProjectionShader : Shader
    {
        private Camera camera;

        public ProjectionShader(IShader shader, Camera camera) : base(shader)
        {
            this.camera = camera;
        }

        public override void Bind()
        {
            base.Bind();
            var model = Matrix4x4.Identity; //Caching is faster than storing in a property.
            var view = camera.GetViewMatrix();
            var projection = camera.ProjectionMatrix;

            NativeShader.SetUniform4(nameof(model), model);
            NativeShader.SetUniform4(nameof(view), view);
            NativeShader.SetUniform4(nameof(projection), projection);
        }
    }
}
