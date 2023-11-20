using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class ChunkLayer : ILayerData
    {
        private byte YLevel;
        private short[] Data;
        private readonly SubChunk Chunk;

        public ChunkLayer(SubChunk parent, byte yLevel)
        {
            Chunk = parent;
            YLevel = yLevel;
            Data = new short[ChunkColumn.SIZE * ChunkColumn.SIZE];
        }

        public BlockState? GetBlock(Vector2i localPosition)
        {
            if(localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE) return null;

            int idx = (localPosition.X * ChunkColumn.SIZE * ChunkColumn.SIZE) + localPosition.Y; //Yes. Y is Z value.
            short blockId = Data[idx];

            Chunk.BlockStates.TryGetValue(blockId, out var blockState);
            return blockState;
        }

        public byte GetYLevel()
        {
            return YLevel;
        }
    }
}
