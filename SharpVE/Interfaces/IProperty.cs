namespace SharpVE.Interfaces
{
    public interface IProperty
    {
        string Name { get; }
    }

    public interface IProperty<T> : IProperty
    {
    }
}
