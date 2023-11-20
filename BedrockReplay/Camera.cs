using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace SharpVE
{
    public class Camera
    {
        #region Private Variables
        //CONSTANTS
        private float SPEED = 8f;
        private float SCREEN_WIDTH;
        private float SCREEN_HEIGHT;
        private float SENSITIVITY = 30f;

        private Vector3 Up = Vector3.UnitY;
        private Vector3 Forward = -Vector3.UnitZ;
        private Vector3 Right = Vector3.UnitX;

        private float Pitch;
        private float Yaw = -90.0f;

        private bool FirstMove = true;
        #endregion

        #region Public Variables
        //Position Variables
        public Vector3 Position;
        public Vector2 LastPosition;
        #endregion

        public Camera(float width, float height, Vector3 position)
        {
            SCREEN_WIDTH = width;
            SCREEN_HEIGHT = height;
            Position = position;
        }

        #region Public Methods
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Forward, Up);
        }
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), SCREEN_WIDTH / SCREEN_HEIGHT, 0.1f, 100f);
        }

        public void InputController(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            if(input.IsKeyDown(Keys.W))
            {
                Position += Forward * SPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.A))
            {
                Position -= Right * SPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.S))
            {
                Position -= Forward * SPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.D))
            {
                Position += Right * SPEED * (float)e.Time;
            }

            if(input.IsKeyDown(Keys.Space))
            {
                Position.Y += SPEED * (float)e.Time;
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                Position.Y -= SPEED * (float)e.Time;
            }

            if(FirstMove)
            {
                LastPosition = new Vector2(mouse.X, mouse.Y);
                FirstMove = false;
            }
            else
            {
                var deltaX = mouse.X - LastPosition.X;
                var deltaY = mouse.Y - LastPosition.Y;
                LastPosition = new Vector2(mouse.X, mouse.Y);

                Yaw += deltaX * SENSITIVITY * (float)e.Time;
                Pitch -= deltaY * SENSITIVITY * (float)e.Time;
            }
            UpdateVectors();
        }
        public void Update(KeyboardState input, MouseState mouse, FrameEventArgs e)
        {
            InputController(input, mouse, e);
        }
        #endregion

        #region Private Methods
        private void UpdateVectors()
        {
            if(Pitch > 89.0f)
            {
                Pitch = 89.0f;
            }
            if (Pitch < -89.0f)
            {
                Pitch = -89.0f;
            }

            Forward.X = MathF.Cos(MathHelper.DegreesToRadians(Pitch)) * MathF.Cos(MathHelper.DegreesToRadians(Yaw));
            Forward.Y = MathF.Sin(MathHelper.DegreesToRadians(Pitch));
            Forward.Z = MathF.Cos(MathHelper.DegreesToRadians(Pitch)) * MathF.Sin(MathHelper.DegreesToRadians(Yaw));

            Forward = Vector3.Normalize(Forward);

            Right = Vector3.Normalize(Vector3.Cross(Forward, Vector3.UnitY));
            Up = Vector3.Normalize(Vector3.Cross(Right, Forward));
        }
        #endregion
    }
}
