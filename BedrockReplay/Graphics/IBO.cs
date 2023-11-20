using OpenTK.Graphics.OpenGL4;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SharpVE.Graphics
{
    public class IBO
    {
        public int ID;
        private List<uint> Data;

        public IBO(List<uint> data)
        {
            Data = data;
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Data.Count * sizeof(uint), Data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, ID);
        public void Unbind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        public void Delete() => GL.DeleteBuffer(ID);
        public int GetCount()
        {
            //return data.Length;
            if (Data == null) return 0;
            return Data.Count();
        }
    }
}
