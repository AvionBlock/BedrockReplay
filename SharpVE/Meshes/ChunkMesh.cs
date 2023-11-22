using SharpVE.Interfaces;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;

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
            var world = Chunk.Chunk.ParentWorld;
            var chunkPosition = new Vector3i(Chunk.Chunk.Position.X * ChunkColumn.SIZE, Chunk.YLevel * ChunkColumn.SIZE, Chunk.Chunk.Position.X * ChunkColumn.SIZE);
            for(int x = 0; x < ChunkColumn.SIZE; x++)
            {
                for (int y = 0; y < ChunkColumn.SIZE; y++)
                {
                    for (int z = 0; z < ChunkColumn.SIZE; z++)
                    {
                        var block = Chunk.GetBlock(new Vector3i(x, y, z));
                        bool px, nx, pz, nz, py, ny;
                        
                        if(x < ChunkColumn.SIZE - 1)
                        {
                            px = world.Registry.GetBlock(Chunk.GetBlock(new Vector3i(x + 1, y, z))?.Name).IsOpaque;
                        }
                        else
                        {
                            //Gotta figure out how the Y pos works...
                            px = world.Registry.GetBlock(world.GetBlock(new Vector3i(x + chunkPosition.X + 1, y + chunkPosition.Y, z + chunkPosition.Z))?.Name).IsOpaque;
                        }
                    }
                }
            }
        }
    }
}