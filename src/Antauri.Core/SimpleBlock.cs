using System;
using Antauri.Transactions;

namespace Antauri.Core
{
    public class SimpleBlock : BlockBase<BlockHeader,SimpleTransactionList>, IEquatable<SimpleBlock>
    {
        public SimpleBlock(long index, string previousHash, long timestamp, SimpleTransactionList data, string hash = "")
        {
            Data = data;
            Hash = hash;
            Header = new BlockHeader()
            {
                Index = index,
                PreviousHash = previousHash,
                TimeStamp = timestamp
            };
        }

        public bool Equals(SimpleBlock other)
        {
            var ret = Header.Equals(other.Header)
                && Data == other.Data
                && Hash == other.Hash;
            return ret;
        }

        public override byte[] GetHashData()
        {
            var value = Header.PreviousHash + Header.Index + Header.TimeStamp;
            var headerBytes =  System.Text.Encoding.UTF8.GetBytes(value);
            var transactionBytes = Data.GetTransactionBytes();
            var bytes = new System.Collections.Generic.List<byte>();
            bytes.AddRange(headerBytes);
            bytes.AddRange(transactionBytes);
            return bytes.ToArray();
        }
    }
}
