namespace SharpVE
{
    class Program
    {
        public static void Main(string[] args)
        {
            using(Game rd = new Game(1280,720))
            {
                rd.VSync = OpenTK.Windowing.Common.VSyncMode.On;
                rd.Run();
            }
        }
    }
}