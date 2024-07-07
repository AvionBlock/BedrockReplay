using BedrockReplay.Core.Interfaces;
using Silk.NET.OpenGL;
using System;
using System.Numerics;

namespace BedrockReplay.OpenGL.Rendering
{
    public class Shader : IShader
    {
        private GL glInstance;
        private uint id;

        public Shader(GL glInstance, string vertexCode, string fragmentCode)
        {
            this.glInstance = glInstance;
            Load(vertexCode, fragmentCode);
        }

        public void Bind()
        {
            glInstance.UseProgram(id);
        }

        public void Reload(string vertexCode, string fragmentCode)
        {
            glInstance.DeleteProgram(id);
            Load(vertexCode, fragmentCode);
        }

        public void SetBool(string name, bool value)
        {
            glInstance.Uniform1(glInstance.GetUniformLocation(id, name), value ? 1 : 0);
        }

        public void SetInt(string name, int value)
        {
            glInstance.Uniform1(glInstance.GetUniformLocation(id, name), value);
        }

        public void SetUInt(string name, uint value)
        {
            glInstance.Uniform1(glInstance.GetUniformLocation(id, name), value);
        }

        public void SetFloat(string name, float value)
        {
            glInstance.Uniform1(glInstance.GetUniformLocation(id, name), value);
        }

        public unsafe void SetUniform4(string name, Matrix4x4 value)
        {
            glInstance.UniformMatrix4(glInstance.GetUniformLocation(id, name), 1, true, (float*)&value);
        }

        public static void Compile(GL glInstance, uint shader)
        {
            glInstance.CompileShader(shader);
            glInstance.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
            if (status != (int)GLEnum.True)
                new Exception("Shader failed to compile: " + glInstance.GetShaderInfoLog(shader));
        }

        private void Load(string vertexCode, string fragmentCode)
        {
            //Compile Vertex Shader
            var vertex = glInstance.CreateShader(ShaderType.VertexShader);
            glInstance.ShaderSource(vertex, vertexCode);
            Compile(glInstance, vertex); //Compile

            //Compile Fragment Shader
            var fragment = glInstance.CreateShader(ShaderType.FragmentShader);
            glInstance.ShaderSource(fragment, fragmentCode);
            Compile(glInstance, fragment);

            id = glInstance.CreateProgram(); //Create the shader itself.

            //attach and link the vertex and fragment shaders to the program.
            glInstance.AttachShader(id, vertex);
            glInstance.AttachShader(id, fragment);
            glInstance.LinkProgram(id);

            //Error Handling.
            glInstance.GetProgram(id, ProgramPropertyARB.LinkStatus, out int lStatus);
            if (lStatus != (int)GLEnum.True)
                throw new Exception("Program failed to link: " + glInstance.GetProgramInfoLog(id));

            //Delete Vertex and Fragment since they are now linked.
            glInstance.DeleteShader(vertex);
            glInstance.DeleteShader(fragment);
        }
    }
}
