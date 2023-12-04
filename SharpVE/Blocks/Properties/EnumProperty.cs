using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public class EnumProperty<E> : IProperty<Enum>
    {
        public string Name { get; }
    }
}
