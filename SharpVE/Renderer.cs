using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpVE.Blocks;
using SharpVE.Graphics;
using SharpVE.Interfaces;
using SharpVE.Meshes;
using SharpVE.WorldSpace;

namespace SharpVE
{
    public class Renderer
    {
        ushort Width, Height;
        public Color4 ClearColor;

        public readonly Camera MainCamera;
        public readonly World MainWorld;
        public readonly List<IShader> Shaders;
        public readonly Texture TextureAtlas;

        public readonly BlockRegistry Blocks;

        public List<ChunkMesh> Meshes;

        public Renderer(ushort width, ushort height, World? world = null)
        {
            Width = width;
            Height = height;
            MainCamera = new Camera(Width, Height, new Vector3(0,0,0));
            Blocks = new BlockRegistry();
            MainWorld = world ?? new World(Blocks.DefaultBlock.GetBlockState(), Blocks);
            Shaders = new List<IShader>();
            Meshes = new List<ChunkMesh>();

            ClearColor = new Color4(0.4f, 0.6f, 0.8f, 1f);

            TextureAtlas = new Texture("atlas.png");

            for(int c = 0; c < MainWorld.Chunks.Count; c++)
            {
                var chunk = MainWorld.Chunks[c];
                for (int i = 0; i < chunk.Sections.Length; i++)
                {
                    var section = chunk.Sections[i];
                    var mesh = new ChunkMesh(section, Blocks);
                    mesh.GenerateMesh();
                    mesh.BuildMesh();
                    Meshes.Add(mesh);
                }
            }

            Blocks.Blocks.Add("grass", new Block("grass"));
        }

        #region Shader Management
        public void AddShader(IShader shader)
        {
            //Thread Safety
            lock (Shaders)
            {
                Shaders.Add(shader);
            }
        }

        public bool RemoveShader(IShader shader)
        {
            //Thread Safety
            lock (Shaders)
            {
                return Shaders.Remove(shader);
            }
        }

        public bool RemoveShaderAt(int index)
        {
            //Thread Safety
            lock (Shaders)
            {
                try
                {
                    Shaders.RemoveAt(index);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void UseShaders()
        {
            lock (Shaders)
            {
                foreach (var shader in Shaders)
                {
                    shader.Use();
                }
            }
        }

        public void DeleteShaders()
        {
            lock (Shaders)
            {
                foreach (var shader in Shaders)
                {
                    shader.Delete();
                }
                Shaders.Clear();
            }
        }
        #endregion

        #region Frame Updates
        public void UpdateFrame(MouseState mouse, KeyboardState keyboard, FrameEventArgs frameEvent)
        {
            MainCamera.Update(keyboard, mouse, frameEvent);
        }

        public void RenderFrame()
        {
            GL.ClearColor(ClearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            UseShaders();
            TextureAtlas.Bind();

            foreach (var mesh in Meshes)
            {
                mesh.Draw();
            }
        }
        #endregion
    }
}
