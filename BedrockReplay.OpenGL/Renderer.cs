using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BedrockReplay.OpenGL
{
    public class Renderer
    {
        public Color ClearColor = Color.MediumAquamarine;

        private IWindow window;
        private GL glInstance;

        private uint shaderProgram;
        private List<Rendering.Shader> shaders = new List<Rendering.Shader>();

        public Renderer(IWindow window)
        {
            this.window = window;
            glInstance = window.CreateOpenGL();

            window.Update += OnUpdate;
            window.Render += OnRender;

            glInstance.ClearColor(ClearColor);
        }

        public Core.Rendering.IShader CreateShader(string vertexCode, string fragmentShader)
        {
            return new Rendering.Shader(glInstance, vertexCode, fragmentShader);
        }

        public void AddShader(Core.Rendering.IShader shader)
        {
            if(shader is Rendering.Shader shaderInstance)
            {
                shaders.Add(shaderInstance);
                return;
            }
            throw new ArgumentException($"{nameof(shader)} is not an instance of {typeof(Rendering.Shader)}");
        }

        public void RemoveShader(Core.Rendering.IShader shader)
        {
            if (shader is Rendering.Shader shaderInstance)
            {
                shaders.Remove(shaderInstance);
                return;
            }
            throw new ArgumentException($"{nameof(shader)} is not an instance of {typeof(Rendering.Shader)}");
        }

        private void OnUpdate(double obj)
        {
            glInstance.Clear(ClearBufferMask.ColorBufferBit);
        }

        private void OnRender(double obj)
        {
            for (int i = 0; i < shaders.Count; i++)
            {
                shaders[i].Use();
            }
        }
    }
}
