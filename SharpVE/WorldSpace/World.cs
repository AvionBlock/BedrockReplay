using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace
{
    public class World
    {
        public List<ChunkColumn> Chunks;
        public BlockState DefaultBlock;

        public World(BlockState defaultBlock)
        {
            Chunks = new List<ChunkColumn>();
            for (int x = 0; x < 20; x++)
            {
                Chunks.Add(new ChunkColumn(new Vector2i(x, 0), this));
            }
            DefaultBlock = defaultBlock;
        }

        public BlockState? GetBlock(Vector3i globalPosition)
        {
            foreach(var chunk in Chunks)
            {
                //Again. vector2i.Y is the Z position
                //Console.WriteLine($"Coordinate {globalPosition} is in {Math.Floor((float)globalPosition.X / ChunkColumn.SIZE)}, {Math.Floor((float)globalPosition.Z / ChunkColumn.SIZE)}");
                if (chunk.Position.X == Math.Floor((float)globalPosition.X / ChunkColumn.SIZE) && chunk.Position.Y == Math.Floor((float)globalPosition.Z / ChunkColumn.SIZE))
                {
                    //Convert global to local position.
                    var x = globalPosition.X % ChunkColumn.SIZE;
                    if(x < 0)
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
    }
}
