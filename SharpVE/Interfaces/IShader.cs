namespace SharpVE.Interfaces
{
    public interface IShader
    {
        public int ID { get; set; }
        public void Use();
        public void Delete();
    }
}
