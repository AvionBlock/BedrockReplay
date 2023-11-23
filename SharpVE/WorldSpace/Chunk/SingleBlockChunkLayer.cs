using SharpVE.Interfaces;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Worlds.Chunks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SingleBlockChunkLayer : ILayerData
    {
        private ushort BlockId;
        public SubChunk Chunk { get; }
        public byte YLevel { get; }

        public SingleBlockChunkLayer(SubChunk chunk, byte yLevel, ushort blockId)
        {
            Chunk = chunk;
            YLevel = yLevel;
            BlockId = blockId;
        }

        public BlockState? GetBlock(Vector2i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE) return null;

            Chunk.BlockStates.TryGetValue(BlockId, out var blockState);
            return blockState;
        }

        public byte GetYLevel()
        {
            return YLevel;
        }
    }
}
