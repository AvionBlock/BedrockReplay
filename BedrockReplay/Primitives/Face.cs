using BedrockReplay.Structures;
using Silk.NET.Maths;

namespace BedrockReplay.Primitives
{
    public class Face
    {
        public readonly Vertex[] vertices =
        [
            new Vertex()
            {
                Position = new Vector3D<float>(0.5f,0.5f,0f),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(0.5f,-0.5f,0f),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(-0.5f,-0.5f,0f),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(-0.5f,0.5f,0f),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            }
        ];

        public readonly uint[] Indices =
        [
            0,1,2,
            2,3,0
        ];
    }
}
