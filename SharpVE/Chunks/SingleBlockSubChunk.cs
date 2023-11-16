using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Interfaces;

namespace SharpVE.Chunks
{
    public class SingleBlockSubChunk : IChunkData
    {
        private ChunkColumn Chunk;
        private byte YIndex;
        private short BlockId;

        public SingleBlockSubChunk(ChunkColumn chunk, byte yIndex)
        {
            Chunk = chunk;
            YIndex = yIndex;
        }

        public Block GetBlock(Vector3i position)
        {
            if (position.X >= ChunkColumn.Width || position.Y >= ChunkColumn.Width || position.Z >= ChunkColumn.Width)
            {
                throw new Exception($"The requested block at {position.X}, {position.Y}, {position.Z} is outside of the subchunk data!");
            }
            return Chunk._World.Registry.GetBlockAtIndex(BlockId);
        }
    }
}
