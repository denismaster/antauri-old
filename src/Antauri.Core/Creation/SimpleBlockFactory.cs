using System;
using System.Text;

namespace Antauri.Core
{
    public class SimpleBlockFactory<TData> : IBlockFactory<TData>
    {
        private readonly IHashProvider _hasher;

        public SimpleBlockFactory(IHashProvider hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public Block CreateBlock(Block lastBlock, TData data)
        {
            int nextIndex = lastBlock.Index + 1;
            long nextTimestamp = DateTime.Now.Ticks;

            var block = new Block(nextIndex, lastBlock.Hash, nextTimestamp, data.ToString());

            _hasher.Hash(block);

            return block;
        }
    }
}
