namespace BedrockReplay.Core.Rendering
{
    public interface IRenderer
    {
        Shader CreateShader(string vertexCode, string fragmentShader);

        IMesh CreateMesh(Vertex[] vertices, uint[] indices);

        void AddShader(Shader shader);

        void RemoveShader(Shader shader);

        void AddMesh(IMesh mesh);

        void RemoveMesh(IMesh mesh);
    }
}
