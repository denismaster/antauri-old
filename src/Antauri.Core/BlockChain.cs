using System;
using System.Collections.Generic;

namespace Antauri.Core
{
    public class BlockChain
    {
        private List<Block> _blocks;

        public BlockChain()
        {
            this._blocks = new List<Block>() { Block.GenesisBlock };
        }

        public List<Block> Blocks => _blocks;
    }
}
