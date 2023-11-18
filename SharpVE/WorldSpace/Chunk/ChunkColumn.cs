using BedrockReplay.Interfaces;
using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.WorldSpace;
using SharpVE.WorldSpace.Chunk;

namespace BedrockReplay.Worlds.Chunks
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
            if (HEIGHT % SIZE != 0) throw new Exception($"Chunk height of {HEIGHT} is not divisible by chunk size of {SIZE}!");

            Sections = new IChunkData[HEIGHT/SIZE];
            Position = position;
            ParentWorld = world;

            for (int i = 0; i < HEIGHT; i++)
            {
                Sections[i] = new SingleBlockSubChunk(this, ParentWorld.Registry.DefaultBlock.GetBlockState(), (short)((i * SIZE) + MINY));
            }
        }

        public BlockState? GetBlock(Vector3i localPosition)
        {
            int yPosSection = localPosition.Y - SIZE;
            if(yPosSection < 0 || yPosSection >= Sections.Length)
            {
                return null;
            }
            localPosition.Y -= SIZE * yPosSection;

            return Sections[yPosSection].GetBlock(localPosition);
        }
    }
}
