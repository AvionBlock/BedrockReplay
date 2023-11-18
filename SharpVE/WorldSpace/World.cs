using BedrockReplay.Worlds.Chunks;
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

        public void Render()
        {

        }
    }
}
