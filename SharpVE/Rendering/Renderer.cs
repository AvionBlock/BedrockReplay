using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SharpVE.Chunks;

namespace SharpVE.Rendering
{
    public class Renderer
    {
        private uint WIDTH = 1280, HEIGHT = 720;
        public Vector3 ViewPosition;

        //Shaders
        private Shader? DefaultShader;

        //Chunk Models
        public List<Model> Models;

        //Initialization
        public Renderer()
        {
            Models = new List<Model>();
            ViewPosition = new Vector3();
        }

        //Setup
        public void Initialize()
        {
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Cw);

            SetupShaders();

            //GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        private void SetupShaders()
        {
            DefaultShader = new Shader("Default.vert", "Default.frag");
        }

        //Chunk Models
        public void LoadChunkModels(World world)
        {
            for(int i = 0; i < world.Chunks.Count; i++)
            {
                //TODO
                if(i == Models.Count)
                {
                    var model = new Model();
                    model.GenerateModel(world.Chunks[i]);
                    model.Setup();
                    Models.Add(model);
                }

                //TODO
                var axis = (Vector3)world.Chunks[i].Position * ChunkColumn.Width-ViewPosition;

                Models[i].Reset();
                Models[i].Translate(axis);
            }
        }

        #region Rendering
        public void Draw(World world)
        {
            DefaultShader?.Bind();

            for(int i = 0; i < Models.Count; i++)
            {
                Models[i].Draw();
            }
        }
        #endregion
    }
}
