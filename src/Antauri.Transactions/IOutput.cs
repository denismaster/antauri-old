namespace Antauri.Transactions
{
    public interface IOutput
    {
        int Index { get; }
        int Value { get; }
        IAddress Address { get; }
    }
}
