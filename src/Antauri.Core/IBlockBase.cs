namespace Antauri.Core
{
    public interface IBlockBase<THeader,TData> : IHashable
        where THeader : IBlockHeader
    {
        THeader Header { get; }
        TData Data { get; }
    }
}
