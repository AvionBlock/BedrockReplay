using BedrockReplay.Graphics;
using OpenTK.Mathematics;

namespace BedrockReplay.World
{
    public class Dimension
    {
        public readonly string Identifier = "minecraft:overworld";

        public List<Chunk> Chunks = new List<Chunk>();

        public Dimension()
        {
            Chunks.Add(new Chunk(this, new Vector3(0,0,0)));
        }

        public void Draw(ShaderProgram shader)
        {
            foreach(var chunk in Chunks)
            {
                chunk.Draw(shader);
            }
        }
    }
}
