using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace BedrockReplay.Interfaces
{
    public interface IChunkData
    {
        public BlockState? GetBlock(Vector3i localPosition);
    }
}
