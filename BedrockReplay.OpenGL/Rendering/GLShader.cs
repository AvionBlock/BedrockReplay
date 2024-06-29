using Silk.NET.OpenGL;
using System;
using System.Net.NetworkInformation;

namespace BedrockReplay.OpenGL.Rendering
{
    public class GLShader
    {
        private Core.Rendering.Shader shader;
        private GL glInstance;

        private uint vertex;
        private uint fragment;

        public GLShader(GL glInstance, Core.Rendering.Shader shader)
        {
            this.glInstance = glInstance;
            this.shader = shader;

            vertex = glInstance.CreateShader(ShaderType.VertexShader);
            glInstance.ShaderSource(vertex, shader.vertexCode);

            glInstance.CompileShader(vertex);
            glInstance.GetShader(vertex, ShaderParameterName.CompileStatus, out int vStatus);
            if (vStatus != (int)GLEnum.True)
                new Exception("Vertex shader failed to compile: " + glInstance.GetShaderInfoLog(vertex));


            fragment = glInstance.CreateShader(ShaderType.FragmentShader);
            glInstance.ShaderSource(fragment, shader.fragmentCode);

            glInstance.CompileShader(fragment);
            glInstance.GetShader(fragment, ShaderParameterName.CompileStatus, out int fStatus);
            if (fStatus != (int)GLEnum.True)
                new Exception("Fragment shader failed to compile: " + glInstance.GetShaderInfoLog(fragment));
        }

        public void AttachShader(uint shaderProgram)
        {
            glInstance.AttachShader(shaderProgram, fragment);
            glInstance.AttachShader(shaderProgram, vertex);

            glInstance.LinkProgram(shaderProgram);

            glInstance.GetProgram(shaderProgram, ProgramPropertyARB.LinkStatus, out int lStatus);
            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + glInstance.GetProgramInfoLog(shaderProgram));
        }

        public void DetachShader(uint shaderProgram)
        {
            glInstance.DetachShader(shaderProgram, fragment);
            glInstance.DetachShader(shaderProgram, vertex);
        }

        public void DeleteShader()
        {
            glInstance.DeleteShader(fragment);
            glInstance.DeleteShader(vertex);
        }
    }
}
