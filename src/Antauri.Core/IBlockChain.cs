using System.Collections.Generic;

namespace Antauri.Core
{
    public interface IBlockChain<TBlock>
        where TBlock: IBasicBlock
    {
        List<TBlock> Blocks { get; }
        TBlock LatestBlock { get; }

        void Add(TBlock newBlock);
        bool IsValidNewBlock(TBlock newBlock, TBlock previousBlock);
        void ReplaceChain(List<TBlock> newBlocks);
    }
}