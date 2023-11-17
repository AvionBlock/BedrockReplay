using OpenTK.Mathematics;

namespace SharpVE.Graphics
{
    public struct Vertex
    {
        public const byte SizeInBytes = 32;

        public readonly Vector3 Position;
        public readonly Vector3 Normal;
        public readonly Vector2 UV;

        public Vertex(Vector3 position, Vector3 normal, Vector2 uv)
        {
            Position = position;
            Normal = normal;
            UV = uv;
        }
    }
}
