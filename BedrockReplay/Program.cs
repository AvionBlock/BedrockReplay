namespace BedrockReplay
{
    class Program
    {
        public static void Main(string[] args)
        {
            using(Renderer rd = new Renderer(500,500))
            {
                rd.Run();
            }
        }
    }
}