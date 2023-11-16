using SharpVE.Blocks;
using SharpVE.Chunks;

namespace SharpVE
{
    public class World
    {
        public List<ChunkColumn> Chunks;
        public Stack<int> UpdateMeshes;
        public BlockRegistry Registry;

        public World()
        {
            Chunks = new List<ChunkColumn>();
            UpdateMeshes = new Stack<int>();
            Registry = new BlockRegistry();
        }
    }
}