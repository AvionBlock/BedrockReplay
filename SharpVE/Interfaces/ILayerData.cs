using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.Interfaces
{
    public interface ILayerData
    {
        public byte GetYLevel();
        public BlockState? GetBlock(Vector2i localPosition);
    }
}
