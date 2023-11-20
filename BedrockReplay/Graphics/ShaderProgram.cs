using OpenTK.Graphics.OpenGL4;

namespace SharpVE.Graphics
{
    public class ShaderProgram
    {
        public int ID;

        public ShaderProgram(string vertexShaderFilepath, string fragmentShaderFilepath)
        {
            ID = GL.CreateProgram();

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            //Add vertex shader source and compile shader
            GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderFilepath));
            GL.CompileShader(vertexShader);

            //Add fragment shader source and compile shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderFilepath));
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

        public void Bind() => GL.UseProgram(ID);
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
