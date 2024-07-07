using BedrockReplay.Core.Interfaces;
using System.Numerics;

namespace BedrockReplay.Core.Rendering
{
    public abstract class Shader : IShader
    {
        public readonly IShader NativeShader;
        public Shader(IShader nativeShader)
        {
            NativeShader = nativeShader;
        }

        public virtual void Reload(string vertex, string fragment)
        {
            NativeShader.Reload(vertex, fragment);
        }

        public virtual void SetBool(string name, bool value)
        {
            NativeShader.SetBool(name, value);
        }

        public virtual void SetInt(string name, int value)
        {
            NativeShader.SetInt(name, value);
        }

        public virtual void SetUInt(string name, uint value)
        {
            NativeShader.SetUInt(name, value);
        }

        public virtual void SetFloat(string name, float value)
        {
            NativeShader.SetFloat(name, value);
        }

        public virtual void SetUniform4(string name, Matrix4x4 value)
        {
            NativeShader.SetUniform4(name, value);
        }

        public virtual void Bind()
        {
            NativeShader.Bind();
        }

        //may add Vec's
    }
}
