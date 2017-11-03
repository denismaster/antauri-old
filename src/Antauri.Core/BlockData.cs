using System;

namespace Antauri.Core
{
    public struct BlockData<TData>
    {
        public int Index;
        public string PreviousHash;
        public long TimeStamp;
        public TData Data;
    }
}
