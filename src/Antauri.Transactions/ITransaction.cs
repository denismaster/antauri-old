using System;
using System.Collections.Generic;

namespace Antauri.Transactions
{
    public interface ITransaction
    {
        IEnumerable<IInput> Inputs {get;}
        IEnumerable<IOutput> Outputs {get;}
    }
}
