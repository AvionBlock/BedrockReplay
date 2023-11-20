using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;

namespace SharpVE.Meshes
{
    public class ChunkMesh
    {
        public IChunkData Chunk;

        public ChunkMesh(IChunkData chunk)
        {
            Chunk = chunk;
        }

        public void BuildMesh()
        {
            for(int x = 0; x < ChunkColumn.SIZE; x++)
            {
                for (int y = 0; y < ChunkColumn.SIZE; y++)
                {
                    for (int z = 0; z < ChunkColumn.SIZE; z++)
                    {
                        var block = Chunk.GetBlock(new OpenTK.Mathematics.Vector3i(x, y, z));
                        Console.WriteLine($"{block?.Name}, {x}, {y}, {z}");
                    }
                }
            }
        }
    }
}
