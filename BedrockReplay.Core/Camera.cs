using System;
using System.Numerics;

namespace BedrockReplay.Core
{
    public class Camera
    {
        public float Speed = 0.5f;
        public float Sensitivity = 0.1f;
        public float FOV = 45.0f;
        public Vector3 Position;
        public Matrix4x4 ProjectionMatrix { get; private set; }

        private float screenWidth;
        private float screenHeight;
        public float pitch;
        public float yaw = 90;
        private Vector2 lastPos;

        Vector3 up = Vector3.UnitY;
        Vector3 front = -Vector3.UnitZ;
        Vector3 right = Vector3.UnitX;

        public Camera(float screenWidth, float screenHeight, Vector3 position)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            Position = position;
            ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(FOV * (MathF.PI / 180), screenWidth / screenHeight, 0.1f, 100.0f);
        }

        public Matrix4x4 GetViewMatrix()
        {
            return Matrix4x4.CreateLookAt(Position, Position + front, up);
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

        public void Update(bool forward, bool backward, bool left, bool right, bool up, bool down, Vector2 mouse, double deltaTime)
        {

            if (forward)
            {
                Position += front * Speed * (float)deltaTime;
            }
            if (left)
            {
                Position -= this.right * Speed * (float)deltaTime;
            }
            if (backward)
            {
                Position -= front * Speed * (float)deltaTime;
            }
            if (right)
            {
                Position += this.right * Speed * (float)deltaTime;
            }
            if (up)
            {
                Position.Y += Speed * (float)deltaTime;
            }
            if (down)
            {
                Position.Y -= Speed * (float)deltaTime;
            }

            var deltaX = mouse.X - lastPos.X;
            var deltaY = mouse.Y - lastPos.Y;
            lastPos = new Vector2(mouse.X, mouse.Y);

            yaw += deltaX * Sensitivity;
            pitch -= deltaY * Sensitivity;

            UpdateVectors();
        }
    }
}
