using BedrockReplay;
using BedrockReplay.Core;
using BedrockReplay.Core.Rendering;
using BedrockReplay.OpenGL;
using BedrockReplay.OpenGL.Rendering;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using System.Numerics;

namespace SharpVE
{
    public class Game
    {
        private string projFrag = @"#version 330 core
out vec4 out_color;

void main() {
	out_color = vec4(1.0,0.5,0.2,1.0);
}";
        private string projVert = @"#version 330 core
layout (location = 0) in vec3 aPosition; //vertex coordinates

//uniform variables
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main() {
	gl_Position = vec4(aPosition, 1.0) * model * view * projection; //Coordinates
}";

        private IWindow window;
        private IInputContext? input;
        private Camera camera;
        private bool forward = false, backward = false, left = false, right = false, up = false, down = false;
        private Vector2 mouseVector;
        public static IRenderer? renderer;
        
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
                input.Mice[i].MouseMove += MouseMove;

            var shader = renderer.CreateShader(projVert, projFrag);
            var projShader = new ProjectionShader(shader, camera);

            renderer.AddShader(shader);
            renderer.AddMesh(renderer.CreateMesh(Mesh.basicTriangleVertices, Mesh.basicTriangleIndices));
        }

        private void OnUpdate(double obj)
        {
            camera.Update(forward, backward, left, right, up, down, mouseVector, obj);
            Console.WriteLine($"{camera.Position} {camera.yaw} {camera.pitch}");
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

            }
        }
    }
}
