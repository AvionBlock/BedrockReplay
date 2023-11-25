using SharpVE.Interfaces;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.WorldSpace;
using SharpVE.WorldSpace.Chunk;

namespace SharpVE.Worlds.Chunks
{
    public class ChunkColumn
    {
        #region Constants
        public const ushort SIZE = 16;
        public const ushort HEIGHT = 256;
        public const short MINY = 0;
        #endregion;

        public readonly Vector2i Position;
        public readonly World ParentWorld;
        public IChunkData[] Sections;

        public ChunkColumn(Vector2i position, World world)
        {
            //Do chunk checks first
            if (SIZE > byte.MaxValue) throw new Exception($"Chunk size of {SIZE} cannot be larger than subchunk byte size of {byte.MaxValue}!");
            if (HEIGHT % SIZE != 0) throw new Exception($"Chunk height of {HEIGHT} is not divisible by chunk size of {SIZE}!");
            if (MINY % SIZE != 0) throw new Exception($"Minimum chunk Y level of {MINY} is not divisible by chunk size of {SIZE}!");

            Sections = new IChunkData[HEIGHT/SIZE];
            Position = position;
            ParentWorld = world;

            for (int i = 0; i < HEIGHT / SIZE; i++)
            {
                Sections[i] = new SingleBlockSubChunk(this, (byte)(i + (MINY / SIZE)), world.DefaultBlock);
            }
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            int yPosSection = localPosition.Y / SIZE;
            if(yPosSection < 0 || yPosSection >= Sections.Length)
            {
                return null;
            }
            localPosition.Y -= SIZE * yPosSection;
            return Sections[yPosSection].GetBlock(localPosition);
        }
    }
}
