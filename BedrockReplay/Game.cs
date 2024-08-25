﻿using Arch.System;
using BedrockReplay.Components;
using BedrockReplay.ComponentSystems;
using BedrockReplay.Managers;
using BedrockReplay.Shaders;
using SharpVE.Blocks;
using SharpVE.Registries;
using Silk.NET.Windowing;
using System.Drawing;

namespace SharpVE
{
    public class Game
    {
        public static World<BlockState> World { get; private set; } = new World<BlockState>();
        public static BlockRegistry BlockRegistry { get; private set; } = new BlockRegistry();

        public static Arch.Core.World ECSWorld = Arch.Core.World.Create();
        public static Group<double> Systems = new Group<double>("systems",
            new CameraSystem(ECSWorld)
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
            var projShader = new ProjectionShader(window.Engine, "./Shaders/Default.vert", "./Shaders/Default.frag");

            ECSWorld.Create(new CameraComponent() { ProjectionShader = projShader }, new TransformComponent());
            //ECSWorld.Create(new MeshRendererComponent(), new TransformComponent());

            Systems.Initialize();
            BlockRegistry.Register(new Block("minecraft:grass", new Dictionary<string, Interfaces.IProperty>()));
            BlockRegistry.Register(new Block("minecraft:air", new Dictionary<string, Interfaces.IProperty>()));
            var b = BlockRegistry.Get("minecraft:grass");
            var b2 = BlockRegistry.Get("minecraft:air");
            World.ChunkManager.CreateChunk(new Data.BlockPosition(0, 0, 0), b.GetDefaultBlockState());
            var chunk = World.ChunkManager.GetChunk(new Data.BlockPosition(0, 0, 0)).SetBlockState(b2.GetDefaultBlockState(), 0, 1, 0);
            var block = chunk.GetBlockState(0, 0, 0);
            Console.WriteLine(chunk);
            Console.WriteLine(block);
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
