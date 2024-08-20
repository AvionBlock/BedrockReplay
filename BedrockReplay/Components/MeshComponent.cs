using AvionEngine.Enums;
using AvionEngine.Interfaces;
using BedrockReplay.Structures;

namespace BedrockReplay.Components
{
    public struct MeshComponent
    {
        public IMesh NativeMesh;

        public MeshComponent(IRenderer renderer, Vertex[] vertices, uint[] indices, UsageMode usageMode = UsageMode.Static, DrawMode drawMode = DrawMode.Triangles)
        {
            NativeMesh = renderer.CreateMesh(vertices, indices, usageMode, drawMode);
        }
    }
}
