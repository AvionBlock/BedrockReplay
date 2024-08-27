using BedrockReplay.Structures;
using Silk.NET.Maths;

namespace BedrockReplay.Primitives
{
    public class Plane
    {
        public Vertex[] vertices =
        {
            new Vertex() 
            { 
                Position = new Vector3D<float>(0,0,0),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(1,0,0),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(1,0,1),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            },
            new Vertex()
            {
                Position = new Vector3D<float>(0,0,1),
                Normal = new Vector3D<float>(0,1,0),
                TexCoord = new Vector2D<float>(0,0)
            }
        };

        public uint[] Indices =
        {
            0,1,2,
            2,3,0
        };
    }
}
