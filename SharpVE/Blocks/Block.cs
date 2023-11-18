using OpenTK.Mathematics;
using SharpVE.Models;

namespace SharpVE.Blocks
{
    public class Block
    {
        public bool IsOpaque = true;
        public bool IsFullCube => Model == null;

        public CustomModel? Model = null;
        public List<Vector2> UV;

        public Block()
        {
            UV = new List<Vector2>();
        }
    }
}
