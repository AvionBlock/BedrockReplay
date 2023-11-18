using OpenTK.Mathematics;

namespace BedrockReplay.Interfaces
{
    public interface ILayerData
    {
        public void GetBlock(Vector2i localPosition);
    }
}
