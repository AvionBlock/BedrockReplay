﻿using BedrockReplay.Core.Interfaces;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace BedrockReplay.OpenGL
{
    public class Renderer : IRenderer
    {
        public Color ClearColor = Color.MediumAquamarine;

        private IWindow window;
        private GL glInstance;

        private List<Core.Interfaces.IShader> shaders = new List<Core.Interfaces.IShader>();
        private List<Rendering.Mesh> meshes = new List<Rendering.Mesh>();

        public Renderer(IWindow window)
        {
            this.window = window;
            glInstance = window.CreateOpenGL();

            window.Update += OnUpdate;
            window.Render += OnRender;

            glInstance.ClearColor(ClearColor);
        }

        public Core.Interfaces.IShader CreateShader(string vertexCode, string fragmentShader)
        {
            return new Rendering.Shader(glInstance, vertexCode, fragmentShader);
        }

        public Core.Rendering.IMesh CreateMesh(Core.Rendering.Vertex[] vertices, uint[] indices)
        {
            return new Rendering.Mesh(glInstance, vertices, indices);
        }

        public void AddShader(Core.Interfaces.IShader shader)
        {
            shaders.Add(shader);
        }

        public void RemoveShader(Core.Interfaces.IShader shader)
        {
            shaders.Remove(shader);
        }

        public void AddMesh(Core.Rendering.IMesh mesh)
        {
            if(mesh is Rendering.Mesh meshInstance)
            {
                meshes.Add(meshInstance);
                return;
            }
            throw new ArgumentException($"{nameof(mesh)} is not an instance of {typeof(Rendering.Mesh)}");
        }

        public void RemoveMesh(Core.Rendering.IMesh mesh)
        {
            if (mesh is Rendering.Mesh meshInstance)
            {
                meshes.Remove(meshInstance);
                return;
            }
            throw new ArgumentException($"{nameof(mesh)} is not an instance of {typeof(Rendering.Mesh)}");
        }

        private void OnUpdate(double obj)
        {
            glInstance.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void OnRender(double obj)
        {
            for (int i = 0; i < shaders.Count; i++)
            {
                shaders[i].Bind();
            }

            for (int i = 0; i < meshes.Count; i++)
            {
                meshes[i].Draw();
            }
        }
    }
}
