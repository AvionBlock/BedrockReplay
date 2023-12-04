using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public class Property<T> : IProperty<T>
    {
        private T? value; //The actual value
        public string Name { get; }

        public T Value
        {
            get => value ?? Default;
            set => this.value = value;
        }
        public T Default { get; set; } = default!;


        public Property(string name)
        {
            Name = name;
        }
    }
}
