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

        private string CalculateHash(int index, string previousHash, long timestamp, string data)
        {
            var builder = new StringBuilder(index);
            builder.Append(previousHash).Append(timestamp).Append(data);
            return _hasher.Hash(builder.ToString());
        }
        public Block CreateBlock(Block lastBlock, string data)
        {
            int nextIndex = lastBlock.Index + 1;
            long nextTimestamp = DateTime.Now.Millisecond;
            string nextHash = CalculateHash(nextIndex, lastBlock.Hash, nextTimestamp, data);
            return new Block(nextIndex, lastBlock.Hash, nextTimestamp, data, nextHash);
        }
    }
}
