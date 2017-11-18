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

        public SimpleBlock CreateBlock(SimpleBlock lastBlock, TData data)
        {
            int nextIndex = lastBlock.Index + 1;
            long nextTimestamp = DateTime.Now.Ticks;

            var block = new SimpleBlock(nextIndex, lastBlock.Hash, nextTimestamp, data.ToString());

            _hasher.Hash(block);

            return block;
        }

        public SimpleBlock CreateGenesisBlock()
            => new SimpleBlock(0, "0", 1465154705, "Genesis Block", "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7");
    }
}
