using Silk.NET.Maths;

namespace BedrockReplay.Components
{
    public struct TransformComponent
    {
        public Vector3D<float> Position;
        public Quaternion<float> Rotation;
        public Vector3D<float> Scale;

        public Matrix4X4<float> Model => Matrix4X4.CreateScale(Scale) * Matrix4X4.CreateFromQuaternion(Rotation) * Matrix4X4.CreateTranslation(Position);

        public TransformComponent()
        {
            Position = new Vector3D<float>();
            Rotation = new Quaternion<float>();
            Scale = new Vector3D<float>();
        }
    }
}