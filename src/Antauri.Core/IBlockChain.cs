using System.Collections.Generic;

namespace Antauri.Core
{
    public interface IBlockChain<TBlock, THeader, TData>
        where TBlock: IBlockBase<THeader,TData>
        where THeader: IBlockHeader
    {
        List<TBlock> Blocks { get; }
        TBlock LatestBlock { get; }

        void Add(TBlock newBlock);
        bool IsValidNewBlock(TBlock newBlock, TBlock previousBlock);
        void ReplaceChain(List<TBlock> newBlocks);
    }
}