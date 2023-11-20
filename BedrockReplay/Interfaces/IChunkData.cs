using SharpVE.Blocks;
using SharpVE.Graphics;

namespace SharpVE.Interfaces
{
    public interface IChunkData
    {
        public Block GetBlock(int localX, int localY, int localZ);
        public void GenerateMesh();
        public void Draw(ShaderProgram shader);
    }
}
