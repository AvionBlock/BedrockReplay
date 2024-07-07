using BedrockReplay.Core.Rendering;

namespace BedrockReplay.Core.Interfaces
{
    public interface IRenderer
    {
        IShader CreateShader(string vertexCode, string fragmentShader);

        IMesh CreateMesh(Vertex[] vertices, uint[] indices);

        void AddShader(IShader shader);

        void RemoveShader(IShader shader);

        void AddMesh(IMesh mesh);

        void RemoveMesh(IMesh mesh);
    }
}
