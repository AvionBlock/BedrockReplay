using Silk.NET.Maths;

namespace BedrockReplay.Components
{
    public struct TransformComponent
    {
        public Vector3D<float> Position;
        public Quaternion<float> Rotation;
        public Vector3D<float> Scale;

        //Credits: https://stackoverflow.com/questions/70462758/c-sharp-how-to-convert-quaternions-to-euler-angles-xyz

        public Vector3D<float> Forward => Vector3D.Multiply(Vector3D<float>.UnitZ, RotationMatrix);
        public Vector3D<float> Backward => Vector3D.Multiply(-Vector3D<float>.UnitZ, RotationMatrix);
        public Vector3D<float> Right => Vector3D.Multiply(-Vector3D<float>.UnitX, RotationMatrix);
        public Vector3D<float> Left => Vector3D.Multiply(Vector3D<float>.UnitX, RotationMatrix);
        public Vector3D<float> Up => Vector3D.Multiply(Vector3D<float>.UnitY, RotationMatrix);
        public Vector3D<float> Down => Vector3D.Multiply(-Vector3D<float>.UnitY, RotationMatrix);

        public Vector3D<float> EulerAngles
        {
            get
            {
                var angles = new Vector3D<float>();

                // roll (x-axis rotation)
                double sinr_cosp = 2 * (Rotation.W * Rotation.X + Rotation.Y * Rotation.Z);
                double cosr_cosp = 1 - 2 * (Rotation.X * Rotation.X + Rotation.Y * Rotation.Y);
                angles.X = (float)Math.Atan2(sinr_cosp, cosr_cosp);

                // pitch (y-axis rotation)
                double sinp = 2 * (Rotation.W * Rotation.Y - Rotation.Z * Rotation.X);
                if (Math.Abs(sinp) >= 1)
                {
                    angles.Y = (float)Math.CopySign(MathF.PI / 2, sinp);
                }
                else
                {
                    angles.Y = (float)Math.Asin(sinp);
                }

                // yaw (z-axis rotation)
                double siny_cosp = 2 * (Rotation.W * Rotation.Z + Rotation.X * Rotation.Y);
                double cosy_cosp = 1 - 2 * (Rotation.Y * Rotation.Y + Rotation.Z * Rotation.Z);
                angles.Z = (float)Math.Atan2(siny_cosp, cosy_cosp);

                return angles;
            }
            set
            {
                float cy = MathF.Cos(value.Z * 0.5f);
                float sy = MathF.Sin(value.Z * 0.5f);
                float cp = MathF.Cos(value.Y * 0.5f);
                float sp = MathF.Sin(value.Y * 0.5f);
                float cr = MathF.Cos(value.X * 0.5f);
                float sr = MathF.Sin(value.X * 0.5f);

                Rotation.W = cr * cp * cy + sr * sp * sy;
                Rotation.X = sr * cp * cy - cr * sp * sy;
                Rotation.Y = cr * sp * cy + sr * cp * sy;
                Rotation.Z = cr * cp * sy - sr * sp * cy;
            }
        }

        public Matrix3X3<float> RotationMatrix => Matrix3X3.CreateFromQuaternion(Rotation);

        public Matrix4X4<float> ModelMatrix => Matrix4X4.CreateScale(Scale) * Matrix4X4.CreateFromQuaternion(Rotation) * Matrix4X4.CreateTranslation(Position);

        public TransformComponent()
        {
            Position = Vector3D<float>.Zero;
            Rotation = Quaternion<float>.Identity;
            Scale = Vector3D<float>.One;
        }

        public TransformComponent(float x, float y, float z)
        {
            Position = new Vector3D<float>(x,y,z);
            Rotation = Quaternion<float>.Identity;
            Scale = Vector3D<float>.One;
        }
    }
}