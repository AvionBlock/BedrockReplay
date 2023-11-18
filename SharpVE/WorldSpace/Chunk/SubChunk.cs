using BedrockReplay.Interfaces;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;

namespace SharpVE.WorldSpace.Chunk
{
    public class SubChunk : IChunkData
    {
        public ILayerData[] Layers;

        //Palette Data - TODO

        public SubChunk()
        {
            Layers = new SingleBlockChunkLayer[ChunkColumn.SIZE * ChunkColumn.SIZE * ChunkColumn.SIZE];
        }

        public void GetBlock(Vector3i localPosition)
        {
            throw new NotImplementedException();
        }
    }
}
