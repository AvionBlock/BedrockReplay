using BedrockReplay.World;
using OpenTK.Mathematics;

namespace BedrockReplay.Textures
{
    public class TextureData
    {
        public static readonly Dictionary<BlockType, Dictionary<Faces, Vector2>> BlockUVs = new Dictionary<BlockType, Dictionary<Faces, Vector2>>()
        {
            { BlockType.Dirt, new Dictionary<Faces, Vector2>()
            {
                { Faces.FRONT, new Vector2(2,15) },
                { Faces.LEFT, new Vector2(2,15) },
                { Faces.RIGHT, new Vector2(2,15) },
                { Faces.BACK, new Vector2(2,15) },
                { Faces.TOP, new Vector2(2,15) },
                { Faces.BOTTOM, new Vector2(2,15) }
            }
            },
            { BlockType.Grass, new Dictionary<Faces, Vector2>()
            {
                { Faces.FRONT, new Vector2(3,15) },
                { Faces.LEFT, new Vector2(3,15) },
                { Faces.RIGHT, new Vector2(3,15) },
                { Faces.BACK, new Vector2(3,15) },
                { Faces.TOP, new Vector2(7,13) },
                { Faces.BOTTOM, new Vector2(2,15) }
            }
            }
        };
    }
}
