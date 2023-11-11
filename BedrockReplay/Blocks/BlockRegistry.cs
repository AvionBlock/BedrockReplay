namespace BedrockReplay.Blocks
{
    public class BlockRegistry
    {
        public List<Block> Blocks = new List<Block>();
        public readonly Block DefaultBlock = new Block(BlockType.Air);

        public BlockRegistry()
        {
            Blocks.Add(DefaultBlock);
            Blocks.Add(new Block(BlockType.Grass));
        }

        public int GetIndexForBlockId(BlockType type)
        {
            return Blocks.FindIndex(x => x.Type == type);
        }
    }
}
