namespace BedrockReplay.Core.Rendering
{
    public interface IRenderer
    {
        IShader CreateShader(string vertexCode, string fragmentShader);
    }
}
