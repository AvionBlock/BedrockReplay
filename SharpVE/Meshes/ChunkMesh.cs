using BedrockReplay.Worlds.Chunks;
using SharpVE.Graphics;
using SharpVE.WorldSpace.Chunk;

namespace SharpVE.Meshes
{
    public class ChunkMesh
    {
        private List<Vertex> Vertices;

        public SubChunk Chunk;

        public ChunkMesh(SubChunk chunk)
        {
            Chunk = chunk;

            Vertices = new List<Vertex>();
        }

        private void BuildMesh()
        {
            for(int i = 0; i < Chunk.Layers.Length; i++)
            {
                var layer = Chunk.Layers[i];
                for(int x = 0; x < ChunkColumn.SIZE; x++)
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
