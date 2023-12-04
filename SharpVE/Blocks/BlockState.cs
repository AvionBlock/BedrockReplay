using SharpVE.Interfaces;

namespace SharpVE.Blocks
{
    public class BlockState
    {
        public Block Block;
        private HashSet<IProperty> Properties;

        public BlockState(Block block)
        {
            Block = block;
            States = new Dictionary<string, dynamic>();
        }

        /*
        public T GetValue<T>(string property, T defaultValue)
        {
            if(States.TryGetValue(property, out var value))
            {
                var result = (T)value;
                if(result != null)
                    return result;
            }
            return defaultValue;
        }

        public void SetValue(string property, dynamic value)
        {
            if (States.TryGetValue(property, out var result))
            {
                if(result.GetType() == value.GetType())
                {
                    States[property] = value;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;

            return obj.GetHashCode().Equals(GetHashCode());
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Block);

            foreach(var value in States)
            {
                hash.Add(value.GetHashCode());
            }

            return hash.ToHashCode();
        }
        */
    }
}
