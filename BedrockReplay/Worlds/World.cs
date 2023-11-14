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
            Chunks.Add(new Chunk(this, new Vector2i(0, 0)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 1)));
            Chunks.Add(new Chunk(this,new Vector2i(0, 2)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 3)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 4)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 5)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 6)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 7)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 8)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 9)));
            Chunks.Add(new Chunk(this, new Vector2i(0, 10)));
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
