using System;
using System.Text;

namespace Antauri.Core
{
    public class SimpleBlockFactory : IBlockFactory<SimpleBlock,string>
    {
        private readonly IHashProvider _hasher;

        public SimpleBlockFactory(IHashProvider hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public SimpleBlock CreateBlock(SimpleBlock lastBlock, string data)
        {
            int nextIndex = lastBlock.Index + 1;
            long nextTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            var block = new SimpleBlock(nextIndex, lastBlock.Hash, nextTimestamp, data);

            _hasher.Hash(block);

            return block;
        }

        public SimpleBlock CreateGenesisBlock()
        {
            var block =  new SimpleBlock(0, "0", 1465154705, "Genesis Block");
            _hasher.Hash(block);
            return block;
        }
    }
}
