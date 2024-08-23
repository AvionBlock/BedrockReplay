using AvionEngine.Rendering;
using SharpVE.Chunks;

namespace BedrockReplay.Components
{
    public struct SubChunkMeshComponent<T> where T : class
    {
        public required BaseMesh Mesh;
        public required SubChunk<T> SubChunk;
    }
}
