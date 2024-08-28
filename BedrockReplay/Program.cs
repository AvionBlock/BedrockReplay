namespace BedrockReplay
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            new Game().Run().GetAwaiter().GetResult();
        }
    }
}