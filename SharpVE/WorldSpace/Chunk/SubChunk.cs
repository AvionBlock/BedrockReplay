using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        public ILayerData[] Layers;
        public Dictionary<short, BlockState> BlockStates;
        public ChunkColumn Chunk;

        public SubChunk(ChunkColumn chunk)
        {
            Layers = new ChunkLayer[ChunkColumn.SIZE * ChunkColumn.SIZE * ChunkColumn.SIZE];
            BlockStates = new Dictionary<short, BlockState>();
            Chunk = chunk;
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            if (localPosition.X >= ChunkColumn.SIZE || localPosition.Y >= ChunkColumn.SIZE || localPosition.Z >= ChunkColumn.SIZE)
            {
                //Temporary
                throw new Exception($"The requested block at {localPosition.X}, {localPosition.Y}, {localPosition.Z} is outside of the subchunk data!");
            }

            foreach(var layer in Layers)
            {
                if (layer.GetYLevel() == localPosition.Y) return layer.GetBlock(new Vector2i(localPosition.X, localPosition.Z));
            }
            return null;
        }
    }
}
