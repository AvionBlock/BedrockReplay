namespace SharpVE.Interfaces
{
    public interface IProperty
    {
        string Name { get; }
    }

    public interface IProperty<T>
    {
        T Value { get; }
        T Default { get; }
    }
}
