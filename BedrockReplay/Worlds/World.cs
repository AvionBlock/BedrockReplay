using SharpVE.Blocks;
using SharpVE.Graphics;
using SharpVE.Worlds.Chunks;
using OpenTK.Mathematics;

namespace SharpVE.Worlds
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
        }

        public Block GetBlock(Vector3 position)
        {
            //Temporary
            return Registry.DefaultBlock;
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
