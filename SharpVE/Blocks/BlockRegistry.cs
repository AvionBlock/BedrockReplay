using OpenTK.Mathematics;

namespace SharpVE.Blocks
{
    public class BlockRegistry
    {
        public readonly Dictionary<string, Block> Blocks;
        public readonly Block DefaultBlock;
        public readonly Block UnknownBlock;

        public BlockRegistry(Block? defaultBlock = null, Block? unknownBlock = null)
        {
            Blocks = new Dictionary<string, Block>();
            DefaultBlock = defaultBlock ?? new Block("air") { IsOpaque = false };
            UnknownBlock = unknownBlock ?? new Block("unknown")
            { UV = new List<Vector2>()
                {
                    new Vector2(1,11),
                    new Vector2(1,12),
                    new Vector2(1,12),
                    new Vector2(1,12),
                    new Vector2(1,12),
                    new Vector2(1,12)
                }
            };
        }

        public Block GetBlock(string? identifier)
        {
            if (identifier == null) return UnknownBlock;

            Blocks.TryGetValue(identifier, out var block);
            if (block == null) return UnknownBlock;
            return block;
        }
    }
}
