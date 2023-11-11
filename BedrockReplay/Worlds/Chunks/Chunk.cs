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
        public readonly World _World;
        public Vector3 Position;
        public int ChunkSections => Sections.Length;

        protected IChunkData?[] Sections;

        public Chunk(Vector3 position, int height, World world)
        {
            Position = position;
            Height = height;

            if (Height % ChunkData.SIZE != 0)
                throw new Exception($"The chunk height of {Height} is not divisible by subchunk height of {ChunkData.SIZE}");

            Sections = new IChunkData[Height / ChunkData.SIZE];
            _World = world;

            for (int i = 0; i < Sections.Length; i++)
            {
                Sections[i] = null;
            }

            Sections[0] = new ChunkData(this, 0);
        }

        public Block GetBlock(int localX, int localY, int localZ)
        {
            IChunkData? subchunk = Sections[(int)(localY - Position.Y) >> 4];
            if (subchunk == null) return _World.Registry.DefaultBlock;
            //Convert to subchunk coordinates and get the block.
            return subchunk.GetBlock(
                (byte)MathF.Abs(localX - Position.X),
                (byte)MathF.Abs(localY - Position.Y),
                (byte)MathF.Abs(localZ - Position.Z));
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
