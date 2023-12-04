using SharpVE.Interfaces;
using System.Collections;

namespace SharpVE.Blocks.States
{
    public class BlockState : IEnumerable<IStateProperty>
    {
        public Block Block;

        protected HashSet<IStateProperty> States;

        public BlockState(Block block)
        {
            Block = block;
            States = new HashSet<IStateProperty>();
        }

        public bool Equals(BlockState blockState)
        {
            var result = Block.Equals(blockState.Block);
            if(!result) return false;

            
        }

        public IEnumerator<IStateProperty> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
