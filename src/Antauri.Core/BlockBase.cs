namespace Antauri.Core
{
    public class BlockBase<THeader, TData>: IBasicBlock<THeader>, IHasData<TData>
        where THeader: IBlockHeader
    {
        public string Hash { get; set; }
        public THeader Header { get; set; }
        public TData Data { get; set; }

        IBlockHeader IBasicBlock.Header => Header;

        public virtual byte[] GetHashData()
        {
            var value = Header.PreviousHash + Header.Index + Header.TimeStamp + Data;
            return System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}
