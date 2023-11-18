using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SingleBlockSubChunk : IChunkData
    {
        private BlockState Block;
        private ChunkColumn Chunk;
        private short YLevel;

        public SingleBlockSubChunk(ChunkColumn chunk, BlockState block, short yLevel)
        {
            Block = block;
            Chunk = chunk;
            YLevel = yLevel;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE || localPosition.Z >= ChunkColumn.SIZE)
            {
                return null;
            }

            return Block;
        }
    }
}
