using SharpVE.Interfaces;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SingleBlockChunkLayer : ILayerData
    {
        private byte YLevel;
        private SubChunk Chunk;
        private short BlockId;
        
        public SingleBlockChunkLayer(SubChunk chunk, byte yLevel, short blockId)
        {
            Chunk = chunk;
            YLevel = yLevel;
            BlockId = blockId;
        }

        public BlockState? GetBlock(Vector2i localPosition)
        {
            Chunk.BlockStates.TryGetValue(BlockId, out var blockState);
            return blockState;
        }

        public byte GetYLevel()
        {
            return YLevel;
        }
    }
}
