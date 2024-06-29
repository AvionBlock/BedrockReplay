using BedrockReplay.Core.Rendering;
using BedrockReplay.OpenGL.Rendering;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BedrockReplay.OpenGL
{
    public class Renderer
    {
        public IWindow window { get; }
        private GL? openGL;

        private uint shaderProgram;
        List<GLMesh> meshes;

        public Renderer(WindowOptions options)
        {
            window = Window.Create(options);

            window.Load += OnLoad;
            window.Update += OnUpdate;
            window.Render += OnRender;

            meshes = new List<GLMesh>();
        }

        public void Run()
        {
            window.Run();
        }

        public void AddShader(Core.Rendering.Shader shader)
        {
            if (openGL == null) return;

            var glShader = new GLShader(openGL, shader);
            glShader.AttachShader(shaderProgram);
            glShader.DetachShader(shaderProgram);
            glShader.DeleteShader();
        }

        public void RenderMesh(Mesh mesh)
        {
            if (openGL == null) return;

            meshes.Add(new GLMesh(openGL, mesh));
        }

        private unsafe void OnLoad()
        {
            openGL = window.CreateOpenGL();
            openGL.ClearColor(Color.Aqua);
            shaderProgram = openGL.CreateProgram();

            const uint positionLoc = 0;
            openGL.EnableVertexAttribArray(positionLoc);
            openGL.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), (void*)0);
            openGL.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }

        private void OnUpdate(double obj)
        {
            openGL?.Clear(ClearBufferMask.ColorBufferBit);
        }

        private unsafe void OnRender(double obj)
        {
            for (int i = 0; i < meshes.Count; i++)
            {
                var mesh = meshes[i];
                openGL?.BindVertexArray(mesh.VAO);
                openGL?.UseProgram(shaderProgram);
                openGL?.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, (void*)0);
            }
        }
    }
}
