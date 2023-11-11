using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Interfaces;

namespace BedrockReplay.Worlds.Chunks
{
    public class SingleBlockChunkData : IChunkData
    {
        private Block _Block;
        private Chunk _Chunk;

        #region Render Pipeline
        VAO? Vao;
        VBO? Vbo;
        VBO? TexVbo;
        VBO? BrightnessVbo;
        IBO? Ibo;
        Texture? ImgTexture;
        #endregion

        public SingleBlockChunkData(Chunk chunkData, Block block)
        {
            _Chunk = chunkData;
            _Block = block;
        }

        public Block GetBlock(int localX, int localY, int localZ, bool throwException = false)
        {
            int idx = localX * Chunk.Width * ChunkData.SIZE + localY * ChunkData.SIZE + localZ;
            if (idx > (Chunk.Width * ChunkData.SIZE * Chunk.Width))
            {
                if (throwException)
                    throw new Exception($"The requested block at {localX}, {localY}, {localZ} is outside of the subchunk data!");
                else
                    return _Chunk._World.Registry.DefaultBlock;
            }
            return _Block;
        }

        public void GenerateMesh()
        {
            throw new NotImplementedException();
        }

        public void Draw(ShaderProgram shader)
        {
            throw new NotImplementedException();
        }
    }
}
