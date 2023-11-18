using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class ChunkLayer : ILayerData
    {
        private short YLevel;
        private short[] Data;
        private readonly SubChunk Chunk;

        public ChunkLayer(SubChunk parent, short yLevel)
        {
            Chunk = parent;
            YLevel = yLevel;
            Data = new short[ChunkColumn.SIZE * ChunkColumn.SIZE];
        }

        public BlockState? GetBlock(Vector2i localPosition)
        {
            int idx = (localPosition.X * ChunkColumn.SIZE * ChunkColumn.SIZE) + localPosition.Y; //Yes. Y is Z value.
            short blockId = Data[idx];

            Chunk.BlockStates.TryGetValue(blockId, out var blockState);
            return blockState;
        }

        public short GetYLevel()
        {
            return YLevel;
        }
    }
}
