using SharpVE.Textures;
using OpenTK.Mathematics;

namespace SharpVE.Blocks
{
    public class Block
    {
        public BlockType Type;

        public Dictionary<Faces, FaceData> Faces;
        public Dictionary<Faces, List<Vector2>> TexUV = new Dictionary<Faces, List<Vector2>>()
        {
            { Blocks.Faces.FRONT, new List<Vector2>() },
            { Blocks.Faces.BACK, new List<Vector2>() },
            { Blocks.Faces.LEFT, new List<Vector2>() },
            { Blocks.Faces.RIGHT, new List<Vector2>() },
            { Blocks.Faces.TOP, new List<Vector2>() },
            { Blocks.Faces.BOTTOM, new List<Vector2>() }
        };

        public Dictionary<Faces, List<Vector2>> GetUVsFromCoordinates(Dictionary<Faces, Vector2> coords, float textureSize = 16)
        {
            var faceData = new Dictionary<Faces, List<Vector2>>();

            foreach (var faceCoord in coords)
            {
                faceData[faceCoord.Key] = new List<Vector2>()
                {
                    new Vector2((faceCoord.Value.X+1f)/textureSize, (faceCoord.Value.Y+1f)/textureSize),
                    new Vector2(faceCoord.Value.X/textureSize, (faceCoord.Value.Y+1f)/textureSize),
                    new Vector2(faceCoord.Value.X/textureSize, faceCoord.Value.Y/textureSize),
                    new Vector2((faceCoord.Value.X+1)/textureSize, faceCoord.Value.Y/textureSize)
                };
            }

            return faceData;
        }

        public Block(BlockType type = BlockType.Air)
        {
            Type = type;

            if (Type != BlockType.Air)
            {
                TexUV = GetUVsFromCoordinates(TextureData.BlockUVs[Type]);
            }

            Faces = new Dictionary<Faces, FaceData>()
            {
                { Blocks.Faces.FRONT, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.FRONT],
                    UV = TexUV[Blocks.Faces.FRONT]
                } },
                { Blocks.Faces.BACK, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.BACK],
                    UV = TexUV[Blocks.Faces.BACK]
                } },
                { Blocks.Faces.LEFT, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.LEFT],
                    UV = TexUV[Blocks.Faces.LEFT]
                } },
                {   Blocks.Faces.RIGHT, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.RIGHT],
                    UV = TexUV[Blocks.Faces.RIGHT]
                } },
                { Blocks.Faces.TOP, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.TOP],
                    UV = TexUV[Blocks.Faces.TOP]
                } },
                {   Blocks.Faces.BOTTOM, new FaceData {
                    Vertices = RawFaceData.rawVertexData[Blocks.Faces.BOTTOM],
                    UV = TexUV[Blocks.Faces.BOTTOM]
                } }
            };
        }

        public FaceData AddFace(Faces face)
        {
            return Faces[face];
        }
    }
}
