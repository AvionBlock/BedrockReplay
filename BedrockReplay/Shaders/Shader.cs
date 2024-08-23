using AvionEngine.Interfaces;
using AvionEngine.Rendering;

namespace BedrockReplay.Shaders
{
    public class Shader : IDisposable
    {
        private readonly IEngine engine;
        private FileSystemWatcher vertexWatcher;
        private FileSystemWatcher fragmentWatcher;

        private string vertexPath;
        private string fragmentPath;

        public readonly BaseShader NativeShader;
        public bool IsDisposed { get; private set; }

        public Shader(IEngine engine, string vertexPath, string fragmentPath)
        {
            this.vertexPath = vertexPath;
            this.fragmentPath = fragmentPath;
            this.engine = engine;

            vertexWatcher = new FileSystemWatcher(Path.GetDirectoryName(vertexPath) ?? "", Path.GetFileName(vertexPath));
            fragmentWatcher = new FileSystemWatcher(Path.GetDirectoryName(fragmentPath) ?? "", Path.GetFileName(fragmentPath));

            vertexWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            fragmentWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            vertexWatcher.Changed += VertexFileChanged;
            fragmentWatcher.Changed += FragmentFileChanged;

            vertexWatcher.EnableRaisingEvents = true;
            fragmentWatcher.EnableRaisingEvents = true;

            NativeShader = engine.CreateShader(File.ReadAllText(vertexPath), File.ReadAllText(fragmentPath));
        }

        public void Render(double delta)
        {
            NativeShader.Render(delta);
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
                NativeShader.NativeShader.Dispose();
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
            engine.Execute(() => NativeShader.Reload(vertexCode, fragmentCode));
        }

        private void FragmentFileChanged(object sender, FileSystemEventArgs e)
        {
            fragmentPath = e.FullPath;
            var vertexCode = File.ReadAllText(vertexPath);
            var fragmentCode = File.ReadAllText(fragmentPath);
            engine.Execute(() => NativeShader.Reload(vertexCode, fragmentCode));
        }
    }
}
