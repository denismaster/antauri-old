using System;
using System.Collections.Generic;

namespace Antauri.Core
{
    public class BlockChainBase<TBlock> : IBlockChain<TBlock> where TBlock : IBasicBlock
    {
        private List<TBlock> _blocks = new List<TBlock>();
        private readonly IHashProvider _hashProvider;
        private readonly TBlock _genesisBlock;

        public BlockChainBase(IHashProvider hashProvider, IGenesisBlockFactory<TBlock> blockFactory)
        {
            _hashProvider = hashProvider;
        }

        public List<TBlock> Blocks => _blocks;

        public TBlock LatestBlock => _blocks[_blocks.Count];

        public virtual void Add(TBlock newBlock)
        {
            if (IsValidNewBlock(newBlock, LatestBlock))
            {
                _blocks.Add(newBlock);
            }
        }

        public virtual bool IsValidNewBlock(TBlock newBlock, TBlock previousBlock)
        {
            if (previousBlock.Header.Index + 1 != newBlock.Header.Index)
            {
                Console.WriteLine("invalid index");
                return false;
            }
            else if (previousBlock.Hash != newBlock.Header.PreviousHash)
            {
                Console.WriteLine("invalid previoushash");
                return false;
            }
            else
            {
                if (!_hashProvider.Verify(newBlock))
                {
                    Console.WriteLine("invalid hash: " + newBlock.Hash);
                    return false;
                }
            }
            return true;
        }

        public virtual void ReplaceChain(List<TBlock> newBlocks)
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

        private bool IsValidBlocks(List<TBlock> newBlocks)
        {
            TBlock firstBlock = newBlocks[0];
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