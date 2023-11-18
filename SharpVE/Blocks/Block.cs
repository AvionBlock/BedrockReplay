using OpenTK.Mathematics;
using SharpVE.Models;

namespace SharpVE.Blocks
{
    public class Block
    {
        public readonly string Name;
        public bool IsOpaque = true;
        public bool IsFullCube => Model == null;

        public CustomModel? Model = null;
        public List<Vector2> UV;

        public Block(string identifer)
        {
            Name = identifer;
            UV = new List<Vector2>();
        }

        public BlockState GetBlockState()
        {
            return new BlockState() { Name = Name };
        }
    }
}
