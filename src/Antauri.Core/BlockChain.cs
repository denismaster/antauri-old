using System;
using System.Collections.Generic;
using System.Text;

namespace Antauri.Core
{
    public class BlockChain
    {
        private List<Block> _blocks;
        private readonly IHashProvider _hasher;

        public BlockChain(IHashProvider hasher)
        {
            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));

            _blocks = new List<Block>() { Block.GenesisBlock };
        }

        public List<Block> Blocks => _blocks;

        public Block LatestBlock => _blocks[_blocks.Count - 1];

        public void addBlock(Block newBlock)
        {
            if (IsValidNewBlock(newBlock, LatestBlock))
            {
                _blocks.Add(newBlock);
            }
        }

        public bool IsValidNewBlock(Block newBlock, Block previousBlock)
        {
            throw new NotImplementedException();
        }

        private string CalculateHash(int index, string previousHash, long timestamp, string data)
        {
            var builder = new StringBuilder(index);
            builder.Append(previousHash).Append(timestamp).Append(data);
            return _hasher.Hash(builder.ToString());
        }

    }
}
