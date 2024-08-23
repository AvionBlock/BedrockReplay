using AvionEngine.Enums;
using AvionEngine.Interfaces;
using BedrockReplay.Structures;

namespace BedrockReplay.Components
{
    public struct SubChunkRendererComponent
    {
        public IMesh NativeMesh;

        public SubChunkRendererComponent(IRenderer renderer, Vertex[] vertices, uint[] indices, UsageMode usageMode = UsageMode.Static, DrawMode drawMode = DrawMode.Triangles)
        {
            NativeMesh = renderer.CreateMesh(vertices, indices, usageMode, drawMode);
        }
    }
}
