namespace Antauri.Core
{
    public interface IBlock : IHashable
    {
        IBlockHeader Header { get; }
    }
    public interface IBlockBase<THeader, TData> : IBlock
        where THeader : IBlockHeader
    {
        new THeader Header { get; }
        TData Data { get; }
    }
}
