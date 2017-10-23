using System;
using System.Text;

namespace Antauri.Core
{
    public class SimpleBlockFactory : IBlockFactory
    {
        private readonly IHashProvider _hasher;

        public SimpleBlockFactory(IHashProvider hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        public Block CreateBlock(Block lastBlock, string data)
        {
            int nextIndex = lastBlock.Index + 1;
            long nextTimestamp = DateTime.Now.Ticks;

            var blockData = new BlockData<string>(){
                Index = nextIndex,
                PreviousHash = lastBlock.Hash,
                TimeStamp = nextTimestamp,
                Data = data
            };

            string nextHash = _hasher.Hash(blockData);
            return new Block(nextIndex, lastBlock.Hash, nextTimestamp, data, nextHash);
        }
    }
}
