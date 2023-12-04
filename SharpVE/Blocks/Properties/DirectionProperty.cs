using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public class DirectionProperty : Property<Direction>
    {

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
