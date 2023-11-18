namespace SharpVE.Blocks
{
    public class BlockState
    {
        public string Name { get; set; } = string.Empty;
        public Dictionary<string, dynamic> States { get; set; } = new Dictionary<string, dynamic>();
    }
}
