using BedrockReplay.Core.Rendering;

namespace BedrockReplay
{
    public class ProjectionShader
    {
        private const string ProjectionVertex = "#version 330 core\r\nlayout (location = 0) in vec3 aPosition; //vertex coordinates\r\nlayout (location = 1) in vec2 aTexCoord; //texture coordinates\r\n\r\nout vec2 texCoord;\r\n\r\n//uniform variables\r\nuniform mat4 model;\r\nuniform mat4 view;\r\nuniform mat4 projection;\r\n\r\nvoid main() {\r\n\tgl_Position = vec4(aPosition, 1.0) * model * view * projection; //Coordinates\r\n\ttexCoord = aTexCoord;\r\n}";
        private const string ProjectionFrag = "#version 330 core\r\n\r\nin vec2 texCoord;\r\n\r\nout vec4 FragColor;\r\n\r\nuniform sampler2D texture0;\r\n\r\nvoid main() {\r\n\tvec4 texel = texture(texture0, texCoord);\r\n\tif(texel.a < 0.5)\r\n\t\tdiscard;\r\n\r\n\tFragColor = texel;\r\n}";

        IShader shader;

        public ProjectionShader(IRenderer renderer)
        {
            shader = renderer.CreateShader(ProjectionVertex, ProjectionFrag);
        }
    }
}
