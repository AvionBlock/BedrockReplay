namespace SharpVE.Blocks
{
    public class BlockRegistry
    {
        public readonly Block DefaultBlock = new Block() { IsAir = true };
        Dictionary<string, Block> Blocks = new Dictionary<string, Block>()
        {
            {
                "minecraft:air", new Block() { Opaque = false, IsAir = true }
            },
            {
                "minecraft:dirt", new Block()
            }
        };

        public Block GetBlockByName(string name)
        {
            Blocks.TryGetValue(name, out Block? block);
            if(block == null)
                return DefaultBlock;
            return block;
        }

        public Block GetBlockAtIndex(int index)
        {
            if(index > Blocks.Count || index < 0) return DefaultBlock;
            return Blocks.Values.ElementAt(index);
        }
    }
}
