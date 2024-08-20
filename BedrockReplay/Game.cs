using BedrockReplay;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Numerics;

namespace SharpVE
{
    public class Game
    {

        private IWindow window;
        private IInputContext? input;
        private bool forward = false, backward = false, left = false, right = false, up = false, down = false;
        private Vector2 mouseVector;
        
        public Game()
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            window = Window.Create(options);
            
            window.Load += OnLoad;
            window.Update += OnUpdate;
            camera = new Camera(window.Size.X, window.Size.Y, new Vector3(0,0,0));

            window.Run();
        }

        private void OnLoad()
        {
            renderer = new Renderer(window);
            input = window.CreateInput();
            for (int i = 0; i < input.Keyboards.Count; i++)
                input.Keyboards[i].KeyDown += KeyDown;
            for (int i = 0; i < input.Keyboards.Count; i++)
                input.Keyboards[i].KeyUp += KeyUp;
            for (int i = 0; i < input.Mice.Count; i++)
            {
                input.Mice[i].MouseMove += MouseMove;
                input.Mice[i].Cursor.CursorMode = CursorMode.Raw;
            }

            var shader = renderer.CreateShader(projVert, projFrag);
            var projShader = new ProjectionShader(shader, camera);

            renderer.AddShader(projShader);
            renderer.AddMesh(renderer.CreateMesh(Mesh.basicTriangleVertices, Mesh.basicTriangleIndices));
            renderer.AddMesh(renderer.CreateMesh(new Vertex[] { new Vertex(1, 0, 0), new Vertex(1, 0.5f, 1), new Vertex(1, 0, 1) }, new uint[] { 0, 1, 2 }));
            renderer.AddMesh(renderer.CreateMesh(new Vertex[] { new Vertex(2, 0, 0), new Vertex(2, 0.5f, 1), new Vertex(0.5f, 0, 1) }, new uint[] { 0, 1, 2 }));
        }

        private void OnUpdate(double obj)
        {
            camera.Update(forward, backward, left, right, up, down, mouseVector, obj);
            //Console.WriteLine($"{camera.Position} {camera.yaw} {camera.pitch}");
        }

        private void MouseMove(IMouse mouse, Vector2 dir)
        {
            mouseVector = dir;
        }

        private void KeyDown(IKeyboard keyboard, Key key, int keyCode)
        {
            switch(key)
            {
                case Key.W:
                    forward = true;
                    break;
                case Key.A:
                    left = true;
                    break;
                case Key.S:
                    backward = true;
                    break;
                case Key.D:
                    right = true;
                    break;
                case Key.Space:
                    up = true;
                    break;
                case Key.ShiftLeft:
                    down = true;
                    break;

            }

            if (key == Key.Escape)
                window.Close();
        }

        private void KeyUp(IKeyboard keyboard, Key key, int keyCode)
        {
            switch (key)
            {
                case Key.W:
                    forward = false;
                    break;
                case Key.A:
                    left = false;
                    break;
                case Key.S:
                    backward = false;
                    break;
                case Key.D:
                    right = false;
                    break;
                case Key.Space:
                    up = false;
                    break;
                case Key.ShiftLeft:
                    down = false;
                    break;

            }
        }
    }
}
