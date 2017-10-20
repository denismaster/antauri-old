using System;

namespace Antauri.Core
{
    public interface IBlockFactory
    {
        Block CreateBlock(Block lastBlock, string data);
    }
}
