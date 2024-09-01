using AvionEngine;
using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace BedrockReplay.Managers
{
    public class WindowInstance
    {
        private AVRenderer? renderer;

        public readonly IWindow Window;
        public AVRenderer Renderer 
        { 
            get
            {
                if (renderer == null)
                    throw new InvalidOperationException("Renderer has not been set!");
                return renderer;
            }
        }

        public delegate void WindowEvent(WindowInstance window);
        public delegate void WindowResizeEvent(WindowInstance window, Vector2D<int> newSize);
        public delegate void WindowFocusChangedEvent(WindowInstance window, bool focused);
        public delegate void WindowUpdateEvent(WindowInstance window, double delta);

        public event WindowEvent? OnWindowClosing;
        public event WindowEvent? OnWindowLoad;
        public event WindowResizeEvent? OnWindowResize;
        public event WindowResizeEvent? OnWindowFramebufferResize;
        public event WindowFocusChangedEvent? OnWindowFocusChanged;
        public event WindowUpdateEvent? OnWindowUpdate;
        public event WindowUpdateEvent? OnWindowRender;

        public WindowInstance(IWindow window)
        {
            Window = window;

            Window.Resize += WindowResize;
            Window.FramebufferResize += WindowFramebufferResize;
            Window.Closing += WindowClosing;
            Window.FocusChanged += WindowFocusChanged;
            Window.Load += WindowLoad;
            Window.Update += WindowUpdate;
            Window.Render += WindowRender;
        }

        public async Task WaitForInitialization()
        {
            while(!Window.IsInitialized)
            {
                await Task.Delay(1);
            }
        }

        public void SetOpenGL()
        {
            renderer = new AvionEngine.OpenGL.GLRenderer(Window);
        }

        public void SetD3D11()
        {
            throw new NotImplementedException();
        }

        public void SetD3D12()
        {
            throw new NotImplementedException();
        }

        private void WindowResize(Vector2D<int> newSize)
        {
            OnWindowResize?.Invoke(this, newSize);
        }

        private void WindowFramebufferResize(Vector2D<int> newSize)
        {
            OnWindowFramebufferResize?.Invoke(this, newSize);
        }

        private void WindowClosing()
        {
            Window.Resize -= WindowResize;
            Window.FramebufferResize -= WindowFramebufferResize;
            Window.Closing -= WindowClosing;
            Window.FocusChanged -= WindowFocusChanged;
            Window.Load -= WindowLoad;
            Window.Update -= WindowUpdate;
            Window.Render -= WindowRender;
            OnWindowClosing?.Invoke(this);
        }

        private void WindowFocusChanged(bool focused)
        {
            OnWindowFocusChanged?.Invoke(this, focused);
        }

        private void WindowLoad()
        {
            OnWindowLoad?.Invoke(this);
        }

        private void WindowUpdate(double delta)
        {
            OnWindowUpdate?.Invoke(this, delta);
        }

        private void WindowRender(double delta)
        {
            OnWindowRender?.Invoke(this, delta);
        }
    }
}
