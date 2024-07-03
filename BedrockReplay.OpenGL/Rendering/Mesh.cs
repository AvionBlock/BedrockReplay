using BedrockReplay.Core.Rendering;
using Silk.NET.OpenGL;
using System;
using System.Runtime.InteropServices;

namespace BedrockReplay.OpenGL.Rendering
{
    public class Mesh : IMesh
    {
        public static Vertex[] basicTriangleVertices = new Vertex[] {
            new Vertex(-0.5f, 0.5f, 0f), // top left vertex - 0
            new Vertex(0.5f, 0.5f, 0f), // top right vertex - 1
            new Vertex(0.5f, -0.5f, 0f), // bottom right - 2
            new Vertex(-0.5f, -0.5f, 0f) // bottom left - 3
        };

        public static uint[] basicTriangleIndices =
            {
                0, 1, 2,
                2, 3, 0
            };

        private GL glInstance;
        public uint[] Indices { get; }

        private uint VBO;
        private uint VAO;
        private uint EBO;

        public unsafe Mesh(GL glInstance, Vertex[] vertices, uint[] indices)
        {
            this.glInstance = glInstance;
            Indices = indices;

            //Create Buffers and Arrays
            VAO = glInstance.GenVertexArray();
            VBO = glInstance.GenBuffer();
            EBO = glInstance.GenBuffer();

            glInstance.BindVertexArray(VAO);

            //Load data
            glInstance.BindBuffer(BufferTargetARB.ArrayBuffer, VBO);
            var vertexes = vertices.AsSpan();
            fixed (float* verticesPtr = MemoryMarshal.Cast<Vertex, float>(vertexes))
                glInstance.BufferData(BufferTargetARB.ArrayBuffer, (UIntPtr)(vertices.Length * sizeof(Vertex)), verticesPtr, BufferUsageARB.StaticDraw);

            glInstance.BindBuffer(BufferTargetARB.ElementArrayBuffer, EBO);
            fixed (uint* indicesPtr = indices)
                glInstance.BufferData(BufferTargetARB.ElementArrayBuffer, (UIntPtr)(indices.Length * sizeof(uint)), indicesPtr, BufferUsageARB.StaticDraw);

            glInstance.EnableVertexAttribArray(0);
            glInstance.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (uint)sizeof(Vertex), (void*)0);

            glInstance.EnableVertexAttribArray(1);
            glInstance.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, (uint)sizeof(Vertex), (void*)Marshal.OffsetOf<Vertex>(nameof(Vertex.Position)));
            
            glInstance.EnableVertexAttribArray(2);
            glInstance.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, (uint)sizeof(Vertex), (void*)Marshal.OffsetOf<Vertex>(nameof(Vertex.TexPosition)));

            glInstance.BindVertexArray(0);
            glInstance.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            glInstance.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
        }

        public unsafe void Draw()
        {
            glInstance.BindVertexArray(VAO);
            glInstance.DrawElements(PrimitiveType.Triangles, (uint)Indices.Length, DrawElementsType.UnsignedInt, (void*)0);
            glInstance.BindVertexArray(0);
        }
    }
}