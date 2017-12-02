namespace Antauri.Core
{
    /// <summary>
    /// The basic block interface
    /// </summary>
    public interface IBasicBlock : IHashable
    {
        IBlockHeader Header { get; }
    }

    /// <summary>
    /// Generic basic block interface with typed header
    /// </summary>
    /// <typeparam name="THeader">Header type</typeparam>
    public interface IBasicBlock<THeader> : IBasicBlock
        where THeader : IBlockHeader
    {
        new THeader Header { get; }
    }
}
