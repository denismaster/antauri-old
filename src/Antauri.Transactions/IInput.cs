namespace Antauri.Transactions
{
    public interface IInput
    {
        int Index { get; }
        string PreviousTransactionHash { get; }
        int PreviousOutputIndex { get; }
    }
}
