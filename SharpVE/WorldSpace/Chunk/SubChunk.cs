using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        private ILayerData[] Layers;
        public ChunkColumn Chunk { get; }
        public sbyte YLevel { get; }

        public readonly Dictionary<ushort, BlockState> BlockStates;

        public SubChunk(ChunkColumn chunk, sbyte yLevel)
        {
            Layers = new SingleBlockChunkLayer[ChunkColumn.SIZE * ChunkColumn.SIZE * ChunkColumn.SIZE];
            BlockStates = new Dictionary<ushort, BlockState>();
            Chunk = chunk;
            YLevel = yLevel;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.Y >= ChunkColumn.SIZE || localPosition.Y < 0)
            {
                return null;
            }

            foreach(var layer in Layers)
            {
                if (layer.GetYLevel() == localPosition.Y) return layer.GetBlock(new Vector2i(localPosition.X, localPosition.Z));
            }
            return null;
        }

        public void SetBlock(Vector3i localPosition, BlockState? block)
        {
            throw new NotImplementedException();
        }

        public Vector3i GetGlobalPosition()
        {
            return new Vector3i(Chunk.Position.X * ChunkColumn.SIZE, YLevel * ChunkColumn.SIZE, Chunk.Position.Y * ChunkColumn.SIZE);
        }
    }
}