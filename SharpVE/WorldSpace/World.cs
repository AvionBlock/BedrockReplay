using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace
{
    public class World
    {
        public List<ChunkColumn> Chunks;
        public BlockRegistry BlockRegistry;

        public World(BlockRegistry registry)
        {
            Chunks = new List<ChunkColumn>();
            BlockRegistry = registry;

            var chunk = new ChunkColumn(new Vector2i(0, 0), this);
            var blockState = BlockRegistry.GetBlock("grass").GetBlockState();
            for (int x = 0; x < ChunkColumn.SIZE; x++)
            {
                for (int y = 0; y < ChunkColumn.SIZE; y++)
                {
                    for (int z = 0; z < ChunkColumn.SIZE; z++)
                    {
                        if(new Random().Next(0,2) == 0)
                        {
                            chunk.SetBlock(new Vector3i(x, y, z), blockState);
                        }
                    }
                }
            }
            Chunks.Add(chunk);
        }

        public BlockState? GetBlock(Vector3i globalPosition)
        {
            foreach (var chunk in Chunks)
            {
                //Again. vector2i.Y is the Z position
                if (chunk.Position.X == Math.Floor((float)globalPosition.X / ChunkColumn.SIZE) && chunk.Position.Y == Math.Floor((float)globalPosition.Z / ChunkColumn.SIZE))
                {
                    //Convert global to local position.
                    var x = globalPosition.X % ChunkColumn.SIZE;
                    if (x < 0)
                    {
                        x += ChunkColumn.SIZE;
                    }

                    var z = globalPosition.Z % ChunkColumn.SIZE;
                    if (z < 0)
                    {
                        z += ChunkColumn.SIZE;
                    }

                    globalPosition.X = x;
                    globalPosition.Y = globalPosition.Y - ChunkColumn.MINY;
                    globalPosition.Z = z;

                    return chunk.GetBlock(globalPosition);
                }
            }
            return null;
        }

        public void SetBlock(Vector3i globalPosition, BlockState block)
        {
            foreach (var chunk in Chunks)
            {
                //Again. vector2i.Y is the Z position
                if (chunk.Position.X == Math.Floor((float)globalPosition.X / ChunkColumn.SIZE) && chunk.Position.Y == Math.Floor((float)globalPosition.Z / ChunkColumn.SIZE))
                {
                    //Convert global to local position.
                    var x = globalPosition.X % ChunkColumn.SIZE;
                    if (x < 0)
                    {
                        x += ChunkColumn.SIZE;
                    }

                    var z = globalPosition.Z % ChunkColumn.SIZE;
                    if (z < 0)
                    {
                        z += ChunkColumn.SIZE;
                    }

                    globalPosition.X = x;
                    globalPosition.Y = globalPosition.Y - ChunkColumn.MINY;
                    globalPosition.Z = z;

                    chunk.SetBlock(globalPosition, block);
                }
            }
        }
    }
}
