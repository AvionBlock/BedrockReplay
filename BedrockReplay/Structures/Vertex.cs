using AvionEngine.Enums;
using AvionEngine.Structures;
using Silk.NET.Maths;

namespace BedrockReplay.Structures
{
    public struct Vertex
    {
        [VertexField(FieldType.Single)]
        public Vector3D<float> Position;
        [VertexField(FieldType.Single)]
        public Vector3D<float> Normal;
        [VertexField(FieldType.Single)]
        public Vector2D<float> TexCoord;
    }
}
