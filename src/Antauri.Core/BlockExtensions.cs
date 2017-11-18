namespace Antauri.Core
{
    public static class BlockExtensions
    {
        public static void Deconstruct<THeader, TData>(this IBlockBase<THeader, TData> block,
            out THeader header, out TData data)
            where THeader: IBlockHeader
        {
            header = block.Header;
            data = block.Data;
        }
    }
}
