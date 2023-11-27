using OpenTK.Graphics.OpenGL4;
using SharpVE.Interfaces;

namespace SharpVE.Graphics
{
    public class Shader : IShader
    {
        public int ID { get; set; }

        public Shader()
        {
            ID = GL.CreateProgram();

            //Link shader program to OpenGL
            GL.LinkProgram(ID);
        }

        public void AddVertex(string vertexShaderPath)
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            //Add vertex shader source and compile shader
            GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderPath));
            GL.CompileShader(vertexShader);

            //Attach Shaders
            GL.AttachShader(ID, vertexShader);

            GL.DeleteShader(vertexShader);
        }

        public void AddFragment(string fragmentShaderPath)
        {
            //Add fragment shader source and compile shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderPath));
            GL.CompileShader(fragmentShader);

            //Attach Shaders
            GL.AttachShader(ID, fragmentShader);

            GL.DeleteShader(fragmentShader);
        }

        public void Use() => GL.UseProgram(ID);
        public void Unbind() => GL.UseProgram(0);
        public void Delete() => GL.DeleteShader(ID);

        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("./Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch
            {
                Console.WriteLine($"Failed to load texture: {filePath}");
            }

            return shaderSource;
        }
    }
}
