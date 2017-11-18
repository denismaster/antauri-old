using System;

namespace Antauri.Core
{
    public interface IBlockFactory<TData>
    {
        SimpleBlock CreateBlock(SimpleBlock lastBlock, TData data);
        SimpleBlock CreateGenesisBlock();
    }
}
