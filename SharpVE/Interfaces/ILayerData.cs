using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.WorldSpace.Chunk;

namespace SharpVE.Interfaces
{
    public interface ILayerData
    {
        public byte YLevel { get; }
        public SubChunk Chunk { get; }
        public BlockState? GetBlock(Vector2i localPosition);
        public void SetBlock(Vector2i localPosition, BlockState state);
    }
}
