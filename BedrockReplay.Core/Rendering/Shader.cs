using System;
using System.Numerics;

namespace BedrockReplay.Core.Rendering
{
    public abstract class Shader
    {
        public virtual void Reload(string vertex, string fragment)
        {
            throw new NotImplementedException();
        }

        public virtual void SetBool(string name, bool value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetInt(string name, int value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetUInt(string name, uint value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetFloat(string name, float value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetUniform4(string name, Matrix4x4 value)
        {
            throw new NotImplementedException();
        }

        public virtual void Bind()
        {
            throw new NotImplementedException();
        }

        //may add Vec's
    }
}
