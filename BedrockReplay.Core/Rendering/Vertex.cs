using System.Numerics;

namespace BedrockReplay.Core.Rendering
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 TexPosition;

        public Vertex(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
            Normal = new Vector3();
            TexPosition = new Vector2();
        }
    }
}
