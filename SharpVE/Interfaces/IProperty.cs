namespace SharpVE.Interfaces
{
    public interface IProperty
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        string Name { get; }
    }

    public interface IProperty<T> : IProperty
    {
        /// <summary>
        /// The value of the property. Returns the default value if null.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// The default value of the property as passed in by the type.
        /// </summary>
        T Default { get; set; }

        /// <summary>
        /// The identifier of the property.
        /// </summary>
        int PropertyId { get; }
    }
}
