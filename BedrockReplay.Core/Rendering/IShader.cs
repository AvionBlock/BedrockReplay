namespace BedrockReplay.Core.Rendering
{
    public interface IShader
    {
        public void Reload(string vertex, string fragment);

        public void SetBool(string name, bool value);

        public void SetInt(string name, int value);

        public void SetUInt(string name, uint value);

        public void SetFloat(string name, float value);

        //may add Vec's
    }
}
