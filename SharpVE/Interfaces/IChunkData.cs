using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.Interfaces
{
    public interface IChunkData
    {
        public Vector3i Position { get; }
        public Block GetBlock(Vector3i position);
    }
}
