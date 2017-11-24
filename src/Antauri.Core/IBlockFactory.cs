using System;

namespace Antauri.Core
{
    public interface IBlockFactory<TData>
    {
        SimpleBlock CreateBlock(SimpleBlock lastBlock, TData data);
        SimpleBlock CreateGenesisBlock();
    }

    public interface IBlockFactory<TBlock,TData> where TBlock : IBasicBlock, IHasData<TData>
    {
        TBlock CreateBlock(TBlock lastBlock, TData data);
        TBlock CreateGenesisBlock();
    }
}
