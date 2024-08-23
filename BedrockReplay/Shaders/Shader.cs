using AvionEngine.Interfaces;
using AvionEngine.Rendering;

namespace BedrockReplay.Shaders
{
    public class Shader : IDisposable
    {
        private readonly FileSystemWatcher vertexWatcher;
        private readonly FileSystemWatcher fragmentWatcher;

        private string vertexPath;
        private string fragmentPath;

        public readonly BaseShader BaseShader;
        public bool IsDisposed { get; private set; }

        public Shader(IEngine engine, string vertexPath, string fragmentPath)
        {
            this.vertexPath = vertexPath;
            this.fragmentPath = fragmentPath;

            vertexWatcher = new FileSystemWatcher(Path.GetDirectoryName(vertexPath) ?? "", Path.GetFileName(vertexPath));
            fragmentWatcher = new FileSystemWatcher(Path.GetDirectoryName(fragmentPath) ?? "", Path.GetFileName(fragmentPath));

            vertexWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fragmentWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            vertexWatcher.Changed += VertexFileChanged;
            fragmentWatcher.Changed += FragmentFileChanged;

            BaseShader = engine.CreateShader(File.ReadAllText(vertexPath), File.ReadAllText(fragmentPath));
            vertexWatcher.EnableRaisingEvents = true;
            fragmentWatcher.EnableRaisingEvents = true;
        }

        public void Render(double delta)
        {
            BaseShader.Render(delta);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) return;

            if (disposing)
            {
                vertexWatcher.Dispose();
                fragmentWatcher.Dispose();
                BaseShader.NativeShader.Dispose();
            }

            IsDisposed = true;
        }

        ~Shader()
        {
            Dispose(false);
        }

        private void VertexFileChanged(object sender, FileSystemEventArgs e)
        {
            vertexPath = e.FullPath;
            var vertexCode = File.ReadAllText(vertexPath);
            var fragmentCode = File.ReadAllText(fragmentPath);
            BaseShader.NativeShader.Renderer.Execute(() => BaseShader.Reload(vertexCode, fragmentCode));
        }

        private void FragmentFileChanged(object sender, FileSystemEventArgs e)
        {
            fragmentPath = e.FullPath;
            var vertexCode = File.ReadAllText(vertexPath);
            var fragmentCode = File.ReadAllText(fragmentPath);
            BaseShader.NativeShader.Renderer.Execute(() => BaseShader.Reload(vertexCode, fragmentCode));
        }
    }
}
