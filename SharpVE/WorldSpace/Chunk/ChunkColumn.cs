using SharpVE.Interfaces;
using OpenTK.Mathematics;
using SharpVE.WorldSpace;
using SharpVE.WorldSpace.Chunk;
using SharpVE.Blocks;

namespace SharpVE.Worlds.Chunks
{
    public class ChunkColumn
    {
        #region Constants
        public const ushort SIZE = 16;
        public const ushort HEIGHT = 256;
        public const short MINY = -64;
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
                Sections[i] = new SingleBlockSubChunk(this, (sbyte)(i + (MINY / SIZE)), world.BlockRegistry.DefaultBlock.GetBlockState());
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

        public void SetBlock(Vector3i localPosition, BlockState block)
        {
            int yPosSection = localPosition.Y / SIZE;
            if (yPosSection < 0 || yPosSection >= Sections.Length)
            {
                return;
            }
            localPosition.Y -= SIZE * yPosSection;
            var section = Sections[yPosSection];
            
            //if single block subchunk. Change to SubChunk else set the block and if the subchunk contains only 1 block, set to single block subchunk.
            if (section is SingleBlockSubChunk)
            {
                var newSection = new SubChunk(this, (sbyte)(yPosSection + (MINY / SIZE)));
                newSection.SetBlock(localPosition, block);
                Sections[yPosSection] = newSection;
            }
            else
            {
                var subchunk = section as SubChunk;
                subchunk?.SetBlock(localPosition, block);
                if (subchunk?.BlockStates.Count == 1)
                {
                    //Set to single block subchunk.
                    Sections[yPosSection] = new SingleBlockSubChunk(this, (sbyte)(yPosSection + (MINY / SIZE)), subchunk.BlockStates.FirstOrDefault().Value);
                }
            }
        }
    }
}
