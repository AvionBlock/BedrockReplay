using Silk.NET.Input;
using Silk.NET.Maths;

namespace BedrockReplay.Components
{
    public struct FPSController
    {
        public float Speed = 0.25f;
        public float Sensitivity = 2.0f;
        public Vector2D<float> LastMousePos = new Vector2D<float>();
        public IKeyboard Keyboard;
        public IMouse Mouse;

        public FPSController(IKeyboard keyboard, IMouse mouse)
        {
            Speed = 0.25f;
            Keyboard = keyboard;
            Mouse = mouse;
            Sensitivity = 2.0f;
            LastMousePos = new Vector2D<float>(mouse.Position.X, mouse.Position.Y);
        }
    }
}