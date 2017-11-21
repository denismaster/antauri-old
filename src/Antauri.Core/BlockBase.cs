namespace Antauri.Core
{
    public class BlockBase<THeader, TData>: IBlockBase<THeader,TData>
        where THeader: IBlockHeader
    {
        public string Hash { get; set; }
        public THeader Header { get; set; }
        public TData Data { get; set; }

        IBlockHeader IBlock.Header => Header;

        public virtual byte[] GetHashData()
        {
            var value = Header.PreviousHash + Header.Index + Header.TimeStamp + Data;
            return System.Text.Encoding.UTF8.GetBytes(value);
        }
    }
}
