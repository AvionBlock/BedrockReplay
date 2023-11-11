using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Worlds.Chunks;
using OpenTK.Mathematics;

namespace BedrockReplay.Worlds
{
    public class World
    {
        public readonly Texture ImgTexture;
        public readonly BlockRegistry Registry;
        public List<Chunk> Chunks = new List<Chunk>();

        public World()
        {
            ImgTexture = new Texture("atlas.png");
            Registry = new BlockRegistry();
            Chunks.Add(new Chunk(new Vector3(0, 0, 0), 256, this));
            Chunks.Add(new Chunk(new Vector3(1, 0, 0), 256, this));
        }

        /// <summary>
        /// Draw the chunk
        /// </summary>
        /// <param name="shader"></param>
        public void Draw(ShaderProgram shader)
        {
            foreach(var chunk in Chunks)
            {
                chunk.Draw(shader);
            }
        }
    }
}
