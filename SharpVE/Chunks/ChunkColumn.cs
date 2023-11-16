using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Interfaces;

namespace SharpVE.Chunks
{
    public class ChunkColumn
    {
        public const int Width = 16;

        public readonly int Height = 256;
        public readonly int MinHeight = 0;
        public readonly World _World;
        public Vector2i Position;
        public int ChunkSections => Sections.Length;

        protected IChunkData[] Sections;

        public ChunkColumn(World world, Vector2i position, int minHeight = 0, int height = 256)
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
                Sections[i] = new SingleBlockSubChunk(this, (byte)i);
            }
        }

        public Block GetBlock(Vector3i position)
        {
            IChunkData subchunk = Sections[position.Y - Position.Y >> 4];
            return subchunk.GetBlock(new Vector3i(
                (byte)MathF.Abs(position.X - Position.X),
                (byte)MathF.Abs(position.Y - Position.Y),
                (byte)MathF.Abs(position.Z - Position.Y)));
        }
    }
}
