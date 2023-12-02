namespace SharpVE.Blocks
{
    public struct BlockState
    {
        public Block Block;
        public Dictionary<string, dynamic> States;

        public BlockState(Block block)
        {
            Block = block;
            States = new Dictionary<string, dynamic>();
        }
    }
}
