namespace BedrockReplay.Core.Rendering
{
    public class Mesh
    {
        public Vertex[] Vertices = new Vertex[] {
            new Vertex(0.5f, 0.5f, 0.0f),
            new Vertex(0.5f, -0.5f, 0.0f),
            new Vertex(-0.5f, -0.5f, 0.0f),
            new Vertex(-0.5f,  0.5f, 0.0f)
        };

        public uint[] Indices =
            {
                0u, 1u, 3u,
                1u, 2u, 3u
            };
    }
}
