using System.Collections.Generic;

namespace Antauri.Core
{
    /// <summary>
    /// Generic blokchain interface
    /// </summary>
    /// <typeparam name="TBlock">Block type</typeparam>
    public interface IBlockChain<TBlock>
        where TBlock: IBasicBlock
    {   
        /// <summary>
        /// List of all blocks in the chain
        /// </summary>
        List<TBlock> Blocks { get; }

        /// <summary>
        /// The latest block in the chain
        /// </summary>
        TBlock LatestBlock { get; }

        /// <summary>
        /// Add new block into the chain
        /// </summary>
        /// <param name="newBlock">block to add</param>
        void Add(TBlock newBlock);

        /// <summary>
        /// Validate new block
        /// </summary>
        /// <param name="newBlock">block to add</param>
        /// <param name="previousBlock">previousBlock of the chain</param>
        /// <returns>true, if block is valid</returns>
        bool IsValidNewBlock(TBlock newBlock, TBlock previousBlock);

        /// <summary>
        /// Replaces blocks in the chain with new blocks
        /// </summary>
        /// <param name="newBlocks">list of new blocks in the blockchain</param>
        void ReplaceChain(List<TBlock> newBlocks);
    }
}