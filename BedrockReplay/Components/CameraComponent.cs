using BedrockReplay.Managers;
using BedrockReplay.Shaders;
using BedrockReplay.Utils;
using Silk.NET.Maths;

namespace BedrockReplay.Components
{
    public struct CameraComponent
    {
        public ProjectionShader ProjectionShader;
        public WindowInstance WindowInstance;
        public float FOV = 60f;
        public float Near = 0.1f;
        public float Far = 100.0f;

        public CameraComponent(ProjectionShader projectionShader, WindowInstance window, float fov = 60f, float near = 0.1f, float far = 100.0f)
        {
            ProjectionShader = projectionShader;
            WindowInstance = window;
            FOV = fov;
            Near = near;
            Far = far;
        }

        public Matrix4X4<float> GetView(TransformComponent transform) => Matrix4X4.CreateLookAt(transform.Position, transform.Position + transform.Forward, transform.Up);
        public Matrix4X4<float> GetProjection() => Matrix4X4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(FOV), WindowInstance.Window.Size.X / (float)WindowInstance.Window.Size.Y, Near, Far);
    }
}
