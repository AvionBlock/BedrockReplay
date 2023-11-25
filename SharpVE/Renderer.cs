using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpVE.Blocks;
using SharpVE.Graphics;
using SharpVE.Meshes;
using SharpVE.WorldSpace;

namespace SharpVE
{
    public class Renderer
    {
        ushort Width, Height;
        public Color4 ClearColor;

        public readonly Camera MainCamera;
        public readonly Shader ProjectionShader;
        public readonly World MainWorld;
        public readonly List<Shader> Shaders;
        public readonly Texture TextureAtlas;

        private Matrix4 ProjectionMatrix;

        public readonly BlockRegistry Blocks;

        public List<ChunkMesh> Meshes;

        public Renderer(ushort width, ushort height, Shader projectionShader, World? world = null)
        {
            Width = width;
            Height = height;
            MainCamera = new Camera(Width, Height, new Vector3(0,0,0));
            ProjectionShader = projectionShader;
            Blocks = new BlockRegistry();
            MainWorld = world ?? new World(Blocks.DefaultBlock.GetBlockState());
            Shaders = new List<Shader>();
            Meshes = new List<ChunkMesh>();

            ClearColor = new Color4(0.4f, 0.6f, 0.8f, 1f);

            TextureAtlas = new Texture("atlas.png");
            
            //Matrix Setup
            ProjectionMatrix = MainCamera.GetProjectionMatrix();
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
        }

        #region Shader Management
        public void AddShader(Shader shader)
        {
            //Thread Safety
            lock (Shaders)
            {
                Shaders.Add(shader);
            }
        }

        public bool RemoveShader(Shader shader)
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
            ProjectionShader.Use();
            TextureAtlas.Bind();

            Matrix4 model = Matrix4.Identity;
            Matrix4 view = MainCamera.GetViewMatrix();

            int modelLocation = GL.GetUniformLocation(ProjectionShader.ID, "model");
            int viewLocation = GL.GetUniformLocation(ProjectionShader.ID, "view");
            int projectionLocation = GL.GetUniformLocation(ProjectionShader.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref ProjectionMatrix);

            foreach (var mesh in Meshes)
            {
                mesh.Draw();
            }
        }
        #endregion
    }
}
