using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;
using OpenTK.Windowing.GraphicsLibraryFramework;
using BedrockReplay.Graphics;

namespace BedrockReplay
{
    public class Renderer : GameWindow
    {
        List<Vector3> vertices = new List<Vector3>()
        {
            //Front Face
            new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, 0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            //Right Face
            new Vector3(0.5f, 0.5f, 0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Bottom Left Vert
            //Back Face
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Left Vert
            //Left Face
            new Vector3(-0.5f, 0.5f, 0.5f), //Top Left Vert
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(-0.5f, -0.5f, -0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            //Top Face
            new Vector3(-0.5f, 0.5f, -0.5f), //Top Left Vert
            new Vector3(0.5f, 0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, 0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, 0.5f, 0.5f), //Bottom Left Vert
            //Bottom Face
            new Vector3(-0.5f, -0.5f, -0.5f), //Top Left Vert
            new Vector3(0.5f, -0.5f, -0.5f), //Top Right Vert
            new Vector3(0.5f, -0.5f, 0.5f), //Bottom Right Vert
            new Vector3(-0.5f, -0.5f, 0.5f), //Bottom Left Vert
            
        };

        List<Vector2> textCoords = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f)
        };

        List<uint> indices = new List<uint>()
        {
            //Front Face
            //Top Triangle
            0, 1, 2,
            //Bottom Triangle
            2, 3, 0,

            //Right Face
            //Top Triangle
            4, 5, 6,
            //Bottom Triangle
            6, 7, 4,

            //Back Face
            //Top Triangle
            8, 9, 10,
            //Bottom Triangle
            10, 11, 8,

            //Left Face
            //Top Triangle
            12, 13, 14,
            //Bottom Triangle
            14, 15, 12,

            //Top Face
            //Top Triangle
            16, 17, 18,
            //Bottom Triangle
            18, 19, 16,

            //Bottom Face
            //Top Triangle
            20, 21, 22,
            //Bottom Triangle
            22, 23, 20,
        };

        //Render Pipeline Variables
        VAO vao;
        IBO ibo;
        ShaderProgram shader;
        Texture texture;

        //Transform variables
        float yRot = 0f;

        //Camera
        Camera camera;

        int Width, Height;
        public Renderer(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Width = width;
            this.Height = height;

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            vao = new VAO();
            var vbo = new VBO(vertices);
            vao.LinkToVao(0, 3, vbo);
            VBO uvVBO = new VBO(textCoords);
            vao.LinkToVao(1, 2, uvVBO);

            ibo = new IBO(indices);
            shader = new ShaderProgram("Default.vert", "Default.frag");
            texture = new Texture("bookshelf.png");

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(Width, Height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
            this.Width = e.Width;
            this.Height = e.Height;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState keyboard = KeyboardState;
            base.OnUpdateFrame(args);
            camera.Update(keyboard, mouse, args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0.4f, 0.6f, 0.8f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Bind();
            vao.Bind();
            ibo.Bind();
            texture.Bind();

            //Transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            model = Matrix4.CreateRotationY(yRot);
            model *= Matrix4.CreateTranslation(0, 0, -3f);

            int modelLocation = GL.GetUniformLocation(shader.ID, "model");
            int viewLocation = GL.GetUniformLocation(shader.ID, "view");
            int projectionLocation = GL.GetUniformLocation(shader.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 4);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            vao.Delete();
            ibo.Delete();
            texture.Delete();
            shader.Delete();
        }
    }
}
