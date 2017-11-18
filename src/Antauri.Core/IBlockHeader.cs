namespace Antauri.Core
{
    public interface IBlockHeader
    {
        long Index { get;}
        string PreviousHash { get; }
        long TimeStamp { get; }
    }
}