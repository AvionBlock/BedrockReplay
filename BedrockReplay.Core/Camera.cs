using System;
using System.Numerics;

namespace BedrockReplay.Core
{
    public class Camera
    {
        public float Speed = 8f;
        public float Sensitivity = 360f;
        public float FOV = 45.0f;
        public Vector3 Position;

        private float screenWidth;
        private float screenHeight;
        private float pitch;
        private float yaw;
        private Matrix4x4 projectionMatrix;

        Vector3 up = Vector3.UnitY;
        Vector3 front = -Vector3.UnitZ;
        Vector3 right = Vector3.UnitX;

        public Camera(float screenWidth, float screenHeight, Vector3 position)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            Position = position;
            projectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FOV * (MathF.PI / 180), screenWidth / screenHeight, 0.1f, 100.0f);
        }

        public Matrix4x4 GetViewMatrix()
        {
            return Matrix4x4.CreateLookAt(Position, Position + front, front);
        }

        private void UpdateVectors()
        {
            if (pitch > 89.0f)
            {
                pitch = 89.0f;
            }
            if (pitch < -89.0f)
            {
                pitch = -89.0f;
            }

            front.X = MathF.Cos(pitch * (MathF.PI / 180)) * MathF.Cos(yaw * (MathF.PI / 180));
            front.Y = MathF.Sin(pitch * (MathF.PI / 180));
            front.Z = MathF.Cos(pitch * (MathF.PI / 180)) * MathF.Sin(yaw * (MathF.PI / 180));

            front = Vector3.Normalize(front);

            right = Vector3.Normalize(Vector3.Cross(front, Vector3.UnitY));
            up = Vector3.Normalize(Vector3.Cross(right, front));
        }
    }
}
