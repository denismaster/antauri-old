using System;

namespace Antauri.Core
{
    public interface IBlockFactory<TData>
    {
        SimpleBlock CreateBlock(SimpleBlock lastBlock, TData data);
        SimpleBlock CreateGenesisBlock();
    }

    /// <summary>
    /// Genesis block factory
    /// </summary>
    /// <typeparam name="TBlock">Genesis block type</typeparam>
    public interface IGenesisBlockFactory<TBlock> where TBlock:IBasicBlock
    {
        /// <summary>
        /// Creates the new genesis block.
        /// </summary>
        /// <returns>Genesis block</returns>
        TBlock CreateGenesisBlock();
    }

    public interface IBlockFactory<TBlock,TData> where TBlock : IBasicBlock, IHasData<TData>
    {
        TBlock CreateBlock(TBlock lastBlock, TData data);
    }
}
