using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Worlds.Chunks;

namespace SharpVE.Interfaces
{
    public interface IChunkData
    {
        public byte YLevel { get; }
        public ChunkColumn Chunk { get; }
        public BlockState? GetBlock(Vector3i localPosition);
        public Vector3i GetGlobalPosition();
    }
}
