using OpenTK.Graphics.OpenGL4;

namespace BedrockReplay.Graphics
{
    public class IBO
    {
        public int ID;

        public IBO(List<uint> data)
        {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Count * sizeof(uint), data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        public void Unbind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        public void Delete() => GL.DeleteBuffer(ID);
    }
}
