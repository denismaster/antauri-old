using System;

namespace Antauri.Core
{
    public class SimpleBlock : BlockBase<BlockHeader,string>, IEquatable<SimpleBlock>
    {
        public int Index { get; private set; }
        public string PreviousHash { get; private set; }
        public long TimeStamp { get; private set; }

        public SimpleBlock(int index, string previousHash, long timestamp, string data, string hash = "")
        {
            Index = index;
            PreviousHash = previousHash;
            TimeStamp = timestamp;
            Data = data;
            Hash = hash;
        }

        public bool Equals(SimpleBlock other)
        {
            var ret = Index == other.Index
                && PreviousHash == other.PreviousHash
                && TimeStamp == other.TimeStamp
                && Data == other.Data
                && Hash == other.Hash;
            return ret;
        }
    }
}
