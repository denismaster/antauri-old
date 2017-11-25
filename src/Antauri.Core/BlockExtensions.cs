namespace Antauri.Core
{
    public static class BlockExtensions
    {
        public static void Deconstruct<TBlock, THeader, TData>(this TBlock block,
            out THeader header, out TData data)
            where TBlock: IBasicBlock<THeader>, IHasData<TData>
            where THeader: IBlockHeader
        {
            header = block.Header;
            data = block.Data;
        }
    }
}
