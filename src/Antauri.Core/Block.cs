using System;

namespace Antauri.Core
{
    public struct Block : IEquatable<Block>
    {
        public static Block GenesisBlock => new Block(0, "0", 1465154705, "Genesis Block", "816534932c2b7154836da6afc367695e6337db8a921823784c14378abed4f7d7");

        public int Index { get; private set; }
        public string PreviousHash { get; private set; }
        public long TimeStamp { get; private set; }
        public string Data { get; private set; }
        public string Hash { get; private set; }

        public Block(int index, string previousHash, long timestamp, string data, string hash)
        {
            Index = index;
            PreviousHash = previousHash;
            TimeStamp = timestamp;
            Data = data;
            Hash = hash;
        }

        public bool Equals(Block other)
        {
            var ret = Index == other.Index
                && PreviousHash == other.PreviousHash
                && TimeStamp == other.TimeStamp
                && Data == other.Data
                && Hash == other.Hash;
            return ret;
        }

        public BlockData<string> BlockData
        {
            get
            {
                return new BlockData<string>()
                {
                    Data = this.Data,
                    Index = this.Index,
                    PreviousHash = this.PreviousHash,
                    TimeStamp = this.TimeStamp
                };
            }
        }
    }
}
