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

namespace SharpVE
{
    public class Game
    {
        public static World<BlockState> World { get; private set; } = new World<BlockState>();

        public static World ECSWorld = Arch.Core.World.Create();
        public static Group<double> Systems = new Group<double>("systems",
            new CameraSystem(ECSWorld),
            new FPSControllerSystem(ECSWorld)
        );
        public Game()
        {
        }

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
            var plane = new BedrockReplay.Primitives.Plane();
            var input = window.Window.CreateInput();
            var keyboard = input.Keyboards.FirstOrDefault();
            var mouse = input.Mice.FirstOrDefault();
            mouse.Cursor.CursorMode = CursorMode.Raw;

            ECSWorld.Create(new CameraComponent(projShader, window), new TransformComponent(0, 0, 0), new FPSController(keyboard, mouse));
            ECSWorld.Create(new ChunkMeshComponent() { Mesh = window.Engine.CreateMesh(plane.vertices, plane.Indices) }, new TransformComponent(1,1,-1) { Rotation = Silk.NET.Maths.Quaternion<float>.CreateFromAxisAngle(new Silk.NET.Maths.Vector3D<float>(0, 0, 1), MathHelper.DegreesToRadians(90)) });

            Systems.Initialize();
;       }

        private void WindowUpdate(WindowInstance window, double delta)
        {
            Systems.BeforeUpdate(delta);
            Systems.Update(delta);
        }

        //Redelegate AfterUpdate to RenderUpdate.
        private void WindowRender(WindowInstance window, double delta)
        {
            Systems.AfterUpdate(delta);
        }
    }
}
