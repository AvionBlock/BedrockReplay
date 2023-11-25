using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SingleBlockSubChunk : IChunkData
    {
        private BlockState Block;
        public ChunkColumn Chunk { get; }
        public byte YLevel { get; }

        public SingleBlockSubChunk(ChunkColumn chunk, byte yLevel, BlockState block)
        {
            Block = block;
            Chunk = chunk;
            YLevel = yLevel;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE || localPosition.Z >= ChunkColumn.SIZE || 
                localPosition.X < 0 || localPosition.Y < 0|| localPosition.Z < 0)
            {
                return null;
            }

            return Block;
        }

        public Vector3i GetGlobalPosition()
        {
            return new Vector3i(Chunk.Position.X * ChunkColumn.SIZE, YLevel * ChunkColumn.SIZE, Chunk.Position.Y * ChunkColumn.SIZE);
        }
    }
}
