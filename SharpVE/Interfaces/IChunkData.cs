using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.Interfaces
{
    public interface IChunkData
    {
        public BlockState? GetBlock(Vector3i localPosition);
    }
}
