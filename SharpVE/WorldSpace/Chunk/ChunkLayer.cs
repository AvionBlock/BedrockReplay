using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class ChunkLayer : ILayerData
    {
        public ushort[] Data { get; }
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

            var blockState = Chunk.BlockStates.ElementAtOrDefault(blockId);
            return blockState;
        }

        public void SetBlock(Vector2i localPosition, BlockState state)
        {
            for(int i = 0; i < Chunk.BlockStates.Count; i++)
            {
                var blockState = Chunk.BlockStates[i];
                if(blockState.Equals(state))
                {
                    int idx = (localPosition.X * ChunkColumn.SIZE) + localPosition.Y; //Yes. Y is Z value.
                    Data[idx] = (ushort)i;
                    return;
                }
            }

            Chunk.BlockStates.Add(state);
            int idz = (localPosition.X * ChunkColumn.SIZE) + localPosition.Y; //Yes. Y is Z value.
            Data[idz] = (ushort)(Chunk.BlockStates.Count - 1);
        }
    }
}
