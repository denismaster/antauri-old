using System;
using System.Collections.Generic;
using System.Text;

namespace Antauri.Core
{
    public class BlockChain: IBlockChain<SimpleBlock>
    {
        private List<SimpleBlock> _blocks;
        private SimpleBlock _genesisBlock;
        private readonly IHashProvider _hasher;

        public BlockChain(IHashProvider hasher, IBlockFactory<SimpleBlock,string> blockFactory)
        {
            if (blockFactory == null)
            {
                throw new ArgumentNullException(nameof(blockFactory));
            }

            _hasher = hasher ?? throw new ArgumentNullException(nameof(hasher));

            _genesisBlock = blockFactory.CreateGenesisBlock();
            
            _blocks = new List<SimpleBlock>() { _genesisBlock };
        }

        public List<SimpleBlock> Blocks => _blocks;

        public SimpleBlock LatestBlock => _blocks[_blocks.Count - 1];

        public void Add(SimpleBlock newBlock)
        {
            if (IsValidNewBlock(newBlock, LatestBlock))
            {
                _blocks.Add(newBlock);
            }
        }

        public bool IsValidNewBlock(SimpleBlock newBlock, SimpleBlock previousBlock)
        {
            if (previousBlock.Index + 1 != newBlock.Index)
            {
                Console.WriteLine("invalid index");
                return false;
            }
            else if (previousBlock.Hash != newBlock.PreviousHash)
            {
                Console.WriteLine("invalid previoushash");
                return false;
            }
            else
            {
                if (!_hasher.Verify(newBlock))
                {
                    Console.WriteLine("invalid hash: " + newBlock.Hash);
                    return false;
                }
            }
            return true;
        }

        public void ReplaceChain(List<SimpleBlock> newBlocks)
        {
            if (IsValidBlocks(newBlocks) && newBlocks.Count > _blocks.Count)
            {
                _blocks = newBlocks;
            }
            else
            {
                Console.WriteLine("Received blockchain invalid");
            }
        }

        private bool IsValidBlocks(List<SimpleBlock> newBlocks)
        {
            SimpleBlock firstBlock = newBlocks[0];
            if (!firstBlock.Equals(_genesisBlock))
            {
                return false;
            }

            for (int i = 1; i < newBlocks.Count; i++)
            {
                if (IsValidNewBlock(newBlocks[i], firstBlock))
                {
                    firstBlock = newBlocks[i];
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

    }
}
