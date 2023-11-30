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
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE ||
                localPosition.X < 0 || localPosition.Y < 0) return null;

            var blockState = Chunk.BlockStates.ElementAtOrDefault(BlockId);
            return blockState;
        }
    }
}
