namespace Antauri.Core
{
    public class BlockHeader : IBlockHeader
    {
        public long Index { get; set; }
        public string PreviousHash { get; set; }
        public long TimeStamp { get; set; }
    }
}
