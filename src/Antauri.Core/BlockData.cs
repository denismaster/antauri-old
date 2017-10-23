using System;

namespace Antauri.Core
{
    public struct BlockData<T>
    {
        public int Index;
        public string PreviousHash;
        public long TimeStamp;
        public T Data;
    }
}
