using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using SharpVE.Graphics;

namespace SharpVE
{
    public class Game : GameWindow
    {
        //Camera
        Camera camera;
        Shader shader;
        Renderer renderer;

        int Width, Height;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.Width = width;
            this.Height = height;

            CenterWindow(new Vector2i(width, height));
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            shader = new Shader("Default.vert", "Default.frag");

            GL.Enable(EnableCap.DepthTest);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Back);

            camera = new Camera(Width, Height, Vector3.Zero);
            CursorState = CursorState.Grabbed;

            renderer = new Renderer((ushort)Width, (ushort)Height, shader);
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
            renderer.UpdateFrame(mouse, keyboard, args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            renderer.RenderFrame();
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            shader.Delete();
        }
    }
}
