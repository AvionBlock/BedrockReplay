using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using System.Drawing;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        private ILayerData[] Layers;
        public ChunkColumn Chunk { get; }
        public sbyte YLevel { get; }

        public readonly List<BlockState> BlockStates;

        public SubChunk(ChunkColumn chunk, sbyte yLevel, BlockState defaultBlock)
        {
            Layers = new SingleBlockChunkLayer[ChunkColumn.SIZE * ChunkColumn.SIZE * ChunkColumn.SIZE];
            BlockStates = new List<BlockState>();
            Chunk = chunk;
            YLevel = yLevel;

            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i] = new SingleBlockChunkLayer(this, (byte)i, 0);
            }
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.Y >= ChunkColumn.SIZE || localPosition.Y < 0)
            {
                return null;
            }

            var layer = Layers.FirstOrDefault(x => x.YLevel == localPosition.Y);
            return layer?.GetBlock(new Vector2i(localPosition.X, localPosition.Z));
        }

        public void SetBlock(Vector3i localPosition, BlockState? block)
        {
            if (localPosition.Y >= ChunkColumn.SIZE || localPosition.Y < 0)
            {
                return;
            }
        }

        public Vector3i GetGlobalPosition()
        {
            return new Vector3i(Chunk.Position.X * ChunkColumn.SIZE, YLevel * ChunkColumn.SIZE, Chunk.Position.Y * ChunkColumn.SIZE);
        }
    }
}