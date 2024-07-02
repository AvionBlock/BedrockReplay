namespace BedrockReplay.Core.Rendering
{
    public interface IMesh
    {
        Vertex[] Vertices { get; set; }
        uint[] Indices { get; set; }
    }
}
