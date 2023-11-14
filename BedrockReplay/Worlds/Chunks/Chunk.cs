using BedrockReplay.Blocks;
using BedrockReplay.Graphics;
using BedrockReplay.Interfaces;
using OpenTK.Mathematics;

namespace BedrockReplay.Worlds.Chunks
{
    public class Chunk
    {
        public const int Width = 16;

        public readonly int Height = 256;
        public readonly int MinHeight = 0;
        public readonly World _World;
        public Vector2i Position;
        public int ChunkSections => Sections.Length;

        protected IChunkData[] Sections;

        public Chunk(World world, Vector2i position, int minHeight = 0, int height = 256)
        {
            Position = position;
            MinHeight = minHeight;
            Height = height;

            if (Height % Width != 0)
                throw new Exception($"The chunk height of {Height} is not divisible by chunk width of {Width}");

            Sections = new IChunkData[Height / Width];
            _World = world;

            for (int i = 0; i < Sections.Length; i++)
            {
                Sections[i] = new SingleBlockChunkData(this, (byte)i);
            }

            Sections[0] = new ChunkData(this, 0);
        }

        public Block GetBlock(int localX, int localY, int localZ)
        {
            IChunkData subchunk = Sections[localY - Position.Y >> 4];
            return subchunk.GetBlock(
                (byte)MathF.Abs(localX - Position.X),
                (byte)MathF.Abs(localY - Position.Y),
                (byte)MathF.Abs(localZ - Position.Y));
        }

        /// <summary>
        /// Draw the chunk
        /// </summary>
        /// <param name="shader"></param>
        public void Draw(ShaderProgram shader)
        {
            foreach(var section in Sections)
            {
                if(section != null)
                {
                    section.Draw(shader);
                }
            }
        }
    }
}
