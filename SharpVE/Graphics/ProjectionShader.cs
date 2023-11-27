using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpVE.Interfaces;

namespace SharpVE.Graphics
{
    public class ProjectionShader : IShader
    {
        public int ID { get; set; }

        public readonly Camera LinkedCamera;
        private Matrix4 ProjectionMatrix;

        public ProjectionShader(string vertexShaderFilepath, string fragmentShaderFilepath, Camera camera)
        {
            LinkedCamera = camera;
            ProjectionMatrix = camera.GetProjectionMatrix();

            ID = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            //Add vertex shader source and compile shader
            GL.ShaderSource(vertexShader, Shader.LoadShaderSource(vertexShaderFilepath));
            GL.CompileShader(vertexShader);

            //Add fragment shader source and compile shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, Shader.LoadShaderSource(fragmentShaderFilepath));
            GL.CompileShader(fragmentShader);

            //Attach Shaders
            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            //Link shader program to OpenGL
            GL.LinkProgram(ID);

            //Delete shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use()
        {
            GL.UseProgram(ID);

            Matrix4 model = Matrix4.Identity;
            Matrix4 view = LinkedCamera.GetViewMatrix();

            int modelLocation = GL.GetUniformLocation(ID, "model");
            int viewLocation = GL.GetUniformLocation(ID, "view");
            int projectionLocation = GL.GetUniformLocation(ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref ProjectionMatrix);
        }

        public void Unbind() => GL.UseProgram(0);
        public void Delete() => GL.DeleteShader(ID);
    }
}
