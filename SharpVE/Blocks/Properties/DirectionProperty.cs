using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public class DirectionProperty<T> : IProperty<EnumProperty<Direction>>
    {
        public string Name { get; }
    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST,
        UP,
        DOWN
    }
}
