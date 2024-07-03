namespace BedrockReplay.Core.Rendering
{
    public interface IRenderer
    {
        Core.Rendering.IShader CreateShader(string vertexCode, string fragmentShader);
    }
}
