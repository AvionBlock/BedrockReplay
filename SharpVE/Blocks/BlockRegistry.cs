using OpenTK.Mathematics;

namespace SharpVE.Blocks
{
    public class BlockRegistry
    {
        public readonly List<Block> Blocks;

        public Block DefaultBlock { get => Blocks[0]; }
        public Block UnknownBlock { get => Blocks[1]; }

        public BlockRegistry(Block? defaultBlock = null, Block? unknownBlock = null)
        {
            Blocks = new List<Block>  {
                {
                    defaultBlock ?? new Block("air") { IsOpaque = false, IsAir = true }
                },
                {
                    unknownBlock ?? new Block("unknown")
                    {
                        UV = new List<Vector2>()
                        {
                            new Vector2(0, 0),
                            new Vector2(0, 0),
                            new Vector2(0, 0),
                            new Vector2(0, 0),
                            new Vector2(0, 0),
                            new Vector2(0, 0)
                        }
                    }
                }
            };
        }

    public Block GetBlock(string? identifier)
    {
        if (identifier == null) return Blocks[1];
        if (identifier == Blocks[0].Name) return Blocks[0];

        var block = Blocks.FirstOrDefault(x => x.Name == identifier);
            if (block == null) return Blocks[1];
        return block;
    }
}
}
