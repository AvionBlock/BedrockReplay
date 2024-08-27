using Silk.NET.Maths;

namespace BedrockReplay.Utils
{
    public static class MathHelper
    {
        public static float DegreesToRadians(float degrees)
        {
            return MathF.PI / 180f * degrees;
        }

        /*
        public static Vector3D<T> QuaternionToEuler<T>(Quaternion<T> quaternion) where T : unmanaged, IFormattable, IEquatable<T>, IComparable<T>
        {
            var angles = new Vector3D<T>();
            // roll (x-axis rotation)

            T sinr_cosp = Scalar.Multiply(Scalar.As<int, T>(2), Scalar.Add(Scalar.Multiply(quaternion.W, quaternion.X), Scalar.Multiply(quaternion.Y, quaternion.Z)));
            T cosr_cosp = Scalar.Subtract(Scalar.As<int, T>(1), Scalar.Multiply(Scalar.As<int, T>(2), Scalar.Add(Scalar.Multiply(quaternion.X, quaternion.X), Scalar.Multiply(quaternion.Y, quaternion.Y))));
            angles.X = Scalar.Atan2(sinr_cosp, cosr_cosp);

            // pitch (y-axis rotation)
            T sinp = Scalar.Multiply(Scalar.As<int, T>(2), Scalar.Subtract(Scalar.Multiply(quaternion.W, quaternion.Y), Scalar.Multiply(quaternion.Z, quaternion.X)));
            if (Scalar.GreaterThanOrEqual(Scalar.Abs(sinp), Scalar.As<int, T>(1)))
            {
                MathF.CopySign
                angles.Y = Scalar.CopySign(MathF.PI / 2, sinp);
            }
            else
            {
                angles.Y = Scalar.Asin(sinp);
            }

            // yaw (z-axis rotation)
            T siny_cosp = Scalar.Multiply(Scalar.As<int, T>(2), Scalar.Add(Scalar.Multiply(quaternion.W, quaternion.Z), Scalar.Multiply(quaternion.X, quaternion.Y)));
            T cosy_cosp = Scalar.Subtract(Scalar.As<int, T>(1), Scalar.Multiply(Scalar.As<int, T>(2), Scalar.Add(Scalar.Multiply(quaternion.Y, quaternion.Y), Scalar.Multiply(quaternion.Z, quaternion.Z))));
            angles.Z = Scalar.Atan2(siny_cosp, cosy_cosp);

            return angles;
        }
        */
    }
}
