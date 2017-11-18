using System;

namespace Antauri.Core
{
    public interface IBlockFactory<TData>
    {
        Block CreateBlock(Block lastBlock, TData data);
        Block CreateGenesisBlock();
    }
}
