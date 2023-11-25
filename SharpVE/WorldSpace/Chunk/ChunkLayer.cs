using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class ChunkLayer : ILayerData
    {
        private ushort[] Data;
        public SubChunk Chunk { get; }
        public byte YLevel { get; }

        public ChunkLayer(SubChunk parent, byte yLevel)
        {
            Chunk = parent;
            YLevel = yLevel;
            Data = new ushort[ChunkColumn.SIZE * ChunkColumn.SIZE];
        }

        public BlockState? GetBlock(Vector2i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE ||
                localPosition.X < 0 || localPosition.Y < 0) return null;

            int idx = (localPosition.X * ChunkColumn.SIZE) + localPosition.Y; //Yes. Y is Z value.
            ushort blockId = Data[idx];

            Chunk.BlockStates.TryGetValue(blockId, out var blockState);
            return blockState;
        }

        public byte GetYLevel()
        {
            return YLevel;
        }
    }
}
