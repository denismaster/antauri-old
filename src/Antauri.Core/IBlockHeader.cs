namespace Antauri.Core
{
    /// <summary>
    /// Basic block header interface
    /// </summary>
    public interface IBlockHeader
    {
        /// <summary>
        /// Block index in the chain. 
        /// </summary>
        long Index { get;}
        /// <summary>
        /// Hash of the previous block
        /// </summary>
        string PreviousHash { get; }
        /// <summary>
        /// Created date and time
        /// </summary>
        long TimeStamp { get; }
    }
}