using Arch.Core;
using Arch.System;
using BedrockReplay.Components;
using BedrockReplay.Utils;
using Silk.NET.Maths;

namespace BedrockReplay.ComponentSystems
{
    public class FPSControllerSystem : BaseSystem<World, double>
    {
        private QueryDescription query;
        public FPSControllerSystem(World world) : base(world)
        {
            query = new QueryDescription().WithAll<TransformComponent, FPSController>();
        }

        public override void Update(in double t)
        {
            var delta = t;
            World.Query(in query, (ref TransformComponent transform, ref FPSController fps) =>
            {
                var moveSpeed = fps.Speed * (float)delta;
                var mVector = Vector2D.Subtract(fps.LastMousePos, new Vector2D<float>(fps.Mouse.Position.X, fps.Mouse.Position.Y));
                fps.LastMousePos = new Vector2D<float>(fps.Mouse.Position.X, fps.Mouse.Position.Y);

                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.Space))
                    transform.Position += Vector3D.Multiply(transform.Up, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.ShiftLeft))
                    transform.Position += Vector3D.Multiply(transform.Down, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.W))
                    transform.Position += Vector3D.Multiply(transform.Forward, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.A))
                    transform.Position += Vector3D.Multiply(transform.Left, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.S))
                    transform.Position += Vector3D.Multiply(transform.Backward, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.D))
                    transform.Position += Vector3D.Multiply(transform.Right, moveSpeed);
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.Q))
                    transform.Rotation *= Quaternion<float>.CreateFromAxisAngle(Vector3D<float>.UnitZ, MathHelper.DegreesToRadians(moveSpeed * 200));
                if (fps.Keyboard.IsKeyPressed(Silk.NET.Input.Key.E))
                    transform.Rotation *= Quaternion<float>.CreateFromAxisAngle(Vector3D<float>.UnitZ, MathHelper.DegreesToRadians(-moveSpeed * 200));

                transform.Rotation *= Quaternion<float>.CreateFromAxisAngle(Vector3D<float>.UnitX, MathHelper.DegreesToRadians(mVector.Y * (float)delta * fps.Sensitivity));
                transform.Rotation *= Quaternion<float>.CreateFromAxisAngle(Vector3D<float>.UnitY, MathHelper.DegreesToRadians(mVector.X * (float)delta * fps.Sensitivity));
            });
        }
    }
}
