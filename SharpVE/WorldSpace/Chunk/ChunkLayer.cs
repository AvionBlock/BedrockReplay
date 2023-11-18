using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;

namespace SharpVE.WorldSpace.Chunk
{
    public class ChunkLayer : ILayerData
    {
        public short[] Data;
        public readonly SubChunk ParentChunk;

        public ChunkLayer(SubChunk parent)
        {
            ParentChunk = parent;
            Data = new short[ChunkColumn.SIZE * ChunkColumn.SIZE];
        }

        public void GetBlock(Vector2i localPosition)
        {
            throw new NotImplementedException();
        }
    }
}
