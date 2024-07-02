using BedrockReplay.Core.Rendering;
using Silk.NET.OpenGL;
using System;
using System.Linq;

namespace BedrockReplay.OpenGL.Rendering
{
    public class Mesh : IMesh
    {
        public static Vertex[] basicTriangleVertices = new Vertex[] {
            new Vertex(0.5f, 0.5f, 0.0f),
            new Vertex(0.5f, -0.5f, 0.0f),
            new Vertex(-0.5f, -0.5f, 0.0f),
            new Vertex(-0.5f,  0.5f, 0.0f)
        };

        public static uint[] basicTriangleIndices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };

        public Vertex[] Vertices { get; set; }
        public uint[] Indices { get; set; }
        private GL glInstance;

        private uint VBO;
        private uint VAO;
        private uint EBO;

        public unsafe Mesh(GL glInstance, Vertex[] vertices, uint[] indices)
        {
            this.glInstance = glInstance;
            Vertices = vertices;
            Indices = indices;

            //Create Buffers and Arrays
            VAO = glInstance.GenVertexArray();
            VBO = glInstance.GenBuffer();
            EBO = glInstance.GenBuffer();

            glInstance.BindVertexArray(VAO);

            //Load data
            glInstance.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            fixed (float* verticesPtr = Vertices.SelectMany(x => new float[] { x.Position.X, x.Position.Y, x.Position.Z }).ToArray())
                glInstance.BufferData(BufferTargetARB.ArrayBuffer, (UIntPtr)(Vertices.Length * sizeof(Vertex)), verticesPtr, BufferUsageARB.StaticDraw);

            glInstance.BindBuffer(BufferTargetARB.ElementArrayBuffer, EBO);
            fixed (uint* indicesPtr = Indices)
                glInstance.BufferData(BufferTargetARB.ArrayBuffer, (UIntPtr)(Indices.Length * sizeof(uint)), indicesPtr, BufferUsageARB.StaticDraw);

            glInstance.EnableVertexAttribArray(0);
            glInstance.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint)sizeof(Vertex), (void*)0);

            glInstance.BindVertexArray(0);
        }

        public unsafe void Draw(Shader shader)
        {
            glInstance.BindVertexArray(VAO);
            glInstance.DrawElements(GLEnum.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
            glInstance.BindVertexArray(0);
        }
    }
}