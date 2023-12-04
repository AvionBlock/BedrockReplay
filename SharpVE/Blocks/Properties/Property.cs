using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public class Property<T> : IProperty<T>
    {
        private T value;

        public T Value => throw new NotImplementedException();
        public T Default => throw new NotImplementedException();
    }
}
