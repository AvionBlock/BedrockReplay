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
        public uint TextureSize = 16;

        public Block(string identifer)
        {
            Name = identifer;
            UV = new List<Vector2>()
            {
                new Vector2(12,7),
                new Vector2(12,7),
                new Vector2(12,7),
                new Vector2(12,7),
                new Vector2(12,7),
                new Vector2(12,7)
            };
        }

        public BlockState GetBlockState()
        {
            return new BlockState() { Name = Name };
        }

        public List<Vector2> GetUVsFromCoordinate(Vector2 position)
        {
            var uvs = new List<Vector2>()
            {
                new Vector2((position.X + 1f) / TextureSize, (position.Y+1f)/TextureSize),
                new Vector2(position.X/TextureSize, (position.Y+1f)/TextureSize),
                new Vector2(position.X/TextureSize, position.Y/TextureSize),
                new Vector2((position.X+1)/TextureSize, position.Y/TextureSize)
            };

            return uvs;
        }
    }
}
