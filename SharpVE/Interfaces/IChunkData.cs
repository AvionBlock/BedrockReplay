using OpenTK.Mathematics;

namespace BedrockReplay.Interfaces
{
    public interface IChunkData
    {
        public void GetBlock(Vector3i localPosition);
    }
}
