using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SingleBlockSubChunk : IChunkData
    {
        public BlockState Block;
        public ChunkColumn Chunk;

        public SingleBlockSubChunk(ChunkColumn chunk, BlockState block)
        {
            Block = block;
            Chunk = chunk;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE || localPosition.Z >= ChunkColumn.SIZE)
            {
                //Temporary
                throw new Exception($"The requested block at {localPosition.X}, {localPosition.Y}, {localPosition.Z} is outside of the subchunk data!");
            }

            return Block;
        }
    }
}
