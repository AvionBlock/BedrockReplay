using SharpVE.Interfaces;

namespace SharpVE.Blocks.Properties
{
    public abstract class Property : Property<object>
    {
        public Property(string name) : base(name) { }
    }

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
        public int PropertyId { get; }


        protected Property(string name)
        {
            Name = name;
            PropertyId = Name.GetHashCode(StringComparison.OrdinalIgnoreCase);
        }

        //Overrides
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return obj.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(GetType().Name, Name, Value);
        }
    }
}
