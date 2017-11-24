namespace Antauri.Core
{
    public interface IBasicBlock : IHashable
    {
        IBlockHeader Header { get; }
    }

    public interface IBasicBlock<THeader> : IBasicBlock
        where THeader : IBlockHeader
    {
        new THeader Header { get; }
    }

    public interface IHasData<TData>
    {
        TData Data { get; }
    }

    public interface IBlockchain<TBlock>
        where TBlock : IBasicBlock
    {

    }
}
