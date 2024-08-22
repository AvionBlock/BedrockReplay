using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace BedrockReplay.Managers
{
    public static class WindowManager
    {
        public static List<WindowInstance> Windows = new List<WindowInstance>();

        public delegate void WindowEvent(WindowInstance window);
        public delegate void WindowResizeEvent(WindowInstance window, Vector2D<int> newSize);
        public delegate void WindowFocusChangedEvent(WindowInstance window, bool focused);
        public delegate void WindowUpdateEvent(WindowInstance window, double delta);

        public static event WindowEvent? OnWindowClosing;
        public static event WindowEvent? OnWindowLoad;
        public static event WindowResizeEvent? OnWindowResize;
        public static event WindowResizeEvent? OnWindowFramebufferResize;
        public static event WindowFocusChangedEvent? OnWindowFocusChanged;
        public static event WindowUpdateEvent? OnWindowUpdate;
        public static event WindowUpdateEvent? OnWindowRender;

        public static WindowInstance CreateWindow(WindowOptions windowOptions)
        {
            var window = Window.Create(windowOptions);
            var windowInstance = new WindowInstance(window);

            windowInstance.OnWindowResize += WindowResize;
            windowInstance.OnWindowFramebufferResize += WindowFramebufferResize;
            windowInstance.OnWindowClosing += WindowClosing;
            windowInstance.OnWindowFocusChanged += WindowFocusChanged;
            windowInstance.OnWindowLoad += WindowLoad;
            windowInstance.OnWindowUpdate += WindowUpdate;
            windowInstance.OnWindowRender += WindowRender;

            Windows.Add(windowInstance);
            return windowInstance;
        }

        public static void BlockingOpenWindows()
        {
            while (Windows.Count > 0)
            {
                Task.Delay(1).Wait();
            }
        }

        private static void WindowResize(WindowInstance window, Vector2D<int> newSize)
        {
            OnWindowResize?.Invoke(window, newSize);
        }

        private static void WindowFramebufferResize(WindowInstance window, Vector2D<int> newSize)
        {
            OnWindowFramebufferResize?.Invoke(window, newSize);
        }

        private static void WindowClosing(WindowInstance window)
        {
            Windows.Remove(window);
            window.OnWindowResize -= WindowResize;
            window.OnWindowFramebufferResize -= WindowFramebufferResize;
            window.OnWindowClosing -= WindowClosing;
            window.OnWindowFocusChanged -= WindowFocusChanged;
            window.OnWindowLoad -= WindowLoad;
            window.OnWindowUpdate -= WindowUpdate;
            window.OnWindowRender -= WindowRender;
            OnWindowClosing?.Invoke(window);
        }

        private static void WindowFocusChanged(WindowInstance window, bool focused)
        {
            OnWindowFocusChanged?.Invoke(window, focused);
        }

        private static void WindowLoad(WindowInstance window)
        {
            OnWindowLoad?.Invoke(window);
        }

        private static void WindowUpdate(WindowInstance window, double delta)
        {
            OnWindowUpdate?.Invoke(window, delta);
        }

        private static void WindowRender(WindowInstance window, double delta)
        {
            OnWindowRender?.Invoke(window, delta);
        }
    }
}
