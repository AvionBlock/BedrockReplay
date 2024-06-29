using BedrockReplay.Core.Rendering;
using Silk.NET.OpenGL;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BedrockReplay.OpenGL.Rendering
{
    public class GLMesh
    {
        private Mesh mesh;
        private GL glInstance;

        private uint VBO;
        private uint VAO;
        private uint EBO;

        public unsafe GLMesh(GL glInstance, Mesh mesh)
        {
            this.mesh = mesh;
            this.glInstance = glInstance;

            VAO = glInstance.GenVertexArray();
            VBO = glInstance.GenBuffer();
            EBO = glInstance.GenBuffer();

            glInstance.BindVertexArray(VAO);

            glInstance.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            fixed (float* vertices = mesh.Vertices.SelectMany(x => new float[] { x.Position.X, x.Position.Y, x.Position.Z }).ToArray())
                glInstance.BufferData(BufferTargetARB.ArrayBuffer, (UIntPtr)(mesh.Vertices.Length * sizeof(Vertex)), vertices, BufferUsageARB.StaticDraw);

            glInstance.BindBuffer(BufferTargetARB.ElementArrayBuffer, EBO);
            fixed (uint* indices = mesh.Indices)
                glInstance.BufferData(BufferTargetARB.ArrayBuffer, (UIntPtr)(mesh.Indices.Length * sizeof(uint)), indices, BufferUsageARB.StaticDraw);

            glInstance.EnableVertexAttribArray(0);
            glInstance.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint)sizeof(Vertex), (void*)0);

            glInstance.BindVertexArray(0);
        }

        public unsafe void Draw(GLShader shader)
        {
            glInstance.BindVertexArray(VAO);
            glInstance.DrawElements(GLEnum.Triangles, (uint)mesh.Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
            glInstance.BindVertexArray(0);
        }
    }
}