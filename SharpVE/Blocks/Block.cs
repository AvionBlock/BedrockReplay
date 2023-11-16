using SharpVE.Mesh;

namespace SharpVE.Blocks
{
    public class Block
    {
        public bool Opaque = true;
        public bool IsAir = false;
        public BlockMesh Mesh;

        public Block()
        {
            Mesh = new BlockMesh();
        }
    }
}
