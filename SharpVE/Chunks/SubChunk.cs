using OpenTK.Mathematics;
using SharpVE.Blocks;
using SharpVE.Interfaces;

namespace SharpVE.Chunks
{
    public class SubChunk : IChunkData
    {
        private short[] Data;
        private ChunkColumn Chunk;
        private byte YIndex;

        //blockstate palette here...
        #region Palettes
        #endregion

        public SubChunk(ChunkColumn chunk, byte yIndex)
        {
            Chunk = chunk;
            YIndex = yIndex;
            Data = new short[(int)MathF.Pow(ChunkColumn.Width, 3)];
        }

        public Block GetBlock(Vector3i position)
        {
            if (position.X >= ChunkColumn.Width || position.Y >= ChunkColumn.Width || position.Z >= ChunkColumn.Width)
            {
                throw new Exception($"The requested block at {position.X}, {position.Y}, {position.Z} is outside of the subchunk data!");
            }
            int idx = (position.X * ChunkColumn.Width * ChunkColumn.Width) + (position.Y * ChunkColumn.Width) + position.Z;
            int blockId = Data[idx];
            return Chunk._World.Registry.GetBlockAtIndex(blockId);
        }
    }
}
