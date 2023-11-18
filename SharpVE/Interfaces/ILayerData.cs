using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace BedrockReplay.Interfaces
{
    public interface ILayerData
    {
        public short GetYLevel();
        public BlockState? GetBlock(Vector2i localPosition);
    }
}
