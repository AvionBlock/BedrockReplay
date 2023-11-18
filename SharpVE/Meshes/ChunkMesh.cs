using BedrockReplay.Worlds.Chunks;
using SharpVE.WorldSpace.Chunk;

namespace SharpVE.Meshes
{
    public class ChunkMesh
    {
        public SubChunk Chunk;

        public ChunkMesh(SubChunk chunk)
        {
            Chunk = chunk;
        }

        private void BuildMesh()
        {
            for(int x = 0; x < ChunkColumn.SIZE; x++)
            {
                for (int y = 0; y < ChunkColumn.SIZE; y++)
                {
                    for (int z = 0; z < ChunkColumn.SIZE; z++)
                    {
                        //TODO
                    }
                }
            }
        }
    }
}
