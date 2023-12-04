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
            Properties = new HashSet<IProperty>();
        }
        
        public IProperty GetProperty(IProperty property)
        {
            foreach(var prop in Properties)
            {
                if(prop.Name == property.Name)
                    return prop;
            }
            return property;
        }

        /*
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
