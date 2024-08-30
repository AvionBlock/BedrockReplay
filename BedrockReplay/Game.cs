using Arch.Core;
using Arch.System;
using BedrockReplay.Components;
using BedrockReplay.ComponentSystems;
using BedrockReplay.Managers;
using BedrockReplay.Shaders;
using BedrockReplay.Utils;
using SharpVE.Blocks;
using Silk.NET.Input;
using Silk.NET.Windowing;
using System.Drawing;
using SharpVE;
using Silk.NET.Maths;

namespace BedrockReplay
{
    public class Game
    {
        public static readonly World<BlockState> World = new World<BlockState>();
        public static readonly World ECSWorld = Arch.Core.World.Create();

        public static readonly Group<double> Systems = new Group<double>("systems",
            new CameraSystem(ECSWorld),
            new FPSControllerSystem(ECSWorld)
        );

        public async Task Run()
        {
            var window = WindowManager.CreateWindow(WindowOptions.Default);
            window.OnWindowLoad += Load;
            window.OnWindowUpdate += WindowUpdate;
            window.OnWindowRender += WindowRender;
            _ = Task.Run(window.Window.Run);
            await WindowManager.BlockingOpenWindows();
        }

        private void Load(WindowInstance window)
        {
            window.SetOpenGL();
            window.Engine.SetClearColor(Color.Aqua);
            window.Engine.Renderer.SetDepthTest(true);
            //window.Engine.Renderer.SetWireframe(true);

            var projShader = new ProjectionShader(window.Engine, "./Shaders/Default.vert", "./Shaders/Default.frag");
            var plane = new Primitives.Face();
            var input = window.Window.CreateInput();
            var keyboard = input.Keyboards.FirstOrDefault();
            var mouse = input.Mice.FirstOrDefault();
            mouse.Cursor.CursorMode = CursorMode.Raw;

            ECSWorld.Create(new CameraComponent(projShader, window), new TransformComponent(0, 0, 0), new FPSController(keyboard, mouse));
            ECSWorld.Create(new ChunkMeshComponent() { Mesh = window.Engine.CreateMesh(plane.vertices, plane.Indices) }, new TransformComponent(0, 0, -1) { Rotation = Silk.NET.Maths.Quaternion<float>.CreateFromAxisAngle(new Silk.NET.Maths.Vector3D<float>(0, 0, 1), MathHelper.DegreesToRadians(90)) });

            Systems.Initialize();
        }

        private static void WindowUpdate(WindowInstance window, double delta)
        {
            var query = new QueryDescription()
                .WithAll<ChunkMeshComponent, TransformComponent>();

            ECSWorld.Query(in query, (ref ChunkMeshComponent chunkMesh, ref TransformComponent transformComponent) =>
            {
                transformComponent.Rotation *= Quaternion<float>.CreateFromAxisAngle(Vector3D<float>.UnitX, MathHelper.DegreesToRadians(1));
            });

            Systems.BeforeUpdate(delta);
            Systems.Update(delta);
        }

        //Re delegated AfterUpdate to RenderUpdate.
        private static void WindowRender(WindowInstance window, double delta)
        {
            Systems.AfterUpdate(delta);
        }
    }
}
