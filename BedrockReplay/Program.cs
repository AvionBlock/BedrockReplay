namespace BedrockReplay
{
    class Program
    {
        public static void Main(string[] args)
        {
            using(Renderer rd = new Renderer(1280,720))
            {
                rd.VSync = OpenTK.Windowing.Common.VSyncMode.On;
                rd.Run();
            }
        }
    }
}