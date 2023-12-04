using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Worlds.Chunks;

namespace SharpVE.Interfaces
{
    public interface IChunkData
    {
        public sbyte YLevel { get; }
        public ChunkColumn Chunk { get; }
        public BlockState? GetBlock(Vector3i localPosition);
        public void SetBlock(Vector3i localPosition, BlockState block);
        public Vector3i GetGlobalPosition();
    }
}
