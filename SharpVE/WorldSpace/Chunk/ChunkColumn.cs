using OpenTK.Mathematics;
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
        public List<SubChunk> Sections;

        public ChunkColumn(Vector2i position)
        {
            //Do chunk checks first
            if (HEIGHT % SIZE != 0) throw new Exception($"Chunk height of {HEIGHT} is not divisible by chunk size of {SIZE}!");

            Sections = new List<SubChunk>();
            Position = position;
        }
    }
}
