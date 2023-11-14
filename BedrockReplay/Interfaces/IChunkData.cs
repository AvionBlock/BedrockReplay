using BedrockReplay.Blocks;
using BedrockReplay.Graphics;

namespace BedrockReplay.Interfaces
{
    public interface IChunkData
    {
        public Block GetBlock(int localX, int localY, int localZ);
        public void GenerateMesh();
        public void Draw(ShaderProgram shader);
    }
}
