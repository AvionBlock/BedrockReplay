using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;
using SharpVE.Blocks;

namespace SharpVE.WorldSpace
{
    public class World
    {
        public List<ChunkColumn> Chunks;
        public BlockRegistry Registry;

        public World()
        {
            Chunks = new List<ChunkColumn>();
            Registry = new BlockRegistry();
        }

        public BlockState? GetBlock(Vector3i globalPosition)
        {
            foreach(var chunk in Chunks)
            {
                //Again. vector2i.Y is the Z position
                if (chunk.Position.X == globalPosition.X / ChunkColumn.SIZE && chunk.Position.Y == globalPosition.Z / ChunkColumn.SIZE)
                {
                    //Convert global to local position.
                    globalPosition.X = Math.Abs(globalPosition.X - (chunk.Position.X * ChunkColumn.SIZE));
                    globalPosition.Y = Math.Abs(globalPosition.Y - ChunkColumn.MINY);
                    globalPosition.Z = Math.Abs(globalPosition.Z - (chunk.Position.Y * ChunkColumn.SIZE));
                    return chunk.GetBlock(globalPosition);
                }
            }
            return null;
        }
    }
}
