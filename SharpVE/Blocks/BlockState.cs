namespace SharpVE.Blocks
{
    public struct BlockState
    {
        public string Name;
        public Dictionary<string, dynamic> States;

        public BlockState()
        {
            Name = string.Empty;
            States = new Dictionary<string, dynamic>();
        }
    }
}
