using AvionEngine.Enums;
using AvionEngine.Structures;
using Silk.NET.Maths;

namespace BedrockReplay.Structures;

public struct CompressedVertex
{
    [VertexField(FieldType.Single)]
    public float Position;
    [VertexField(FieldType.Single)]
    public float Normal;
    [VertexField(FieldType.Single)]
    public Vector2D<float> TexCoord;
}