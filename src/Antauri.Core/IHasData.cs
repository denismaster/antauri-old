namespace Antauri.Core
{
    /// <summary>
    /// Has some Data object
    /// </summary>
    /// <typeparam name="TData">Data object type</typeparam>
    public interface IHasData<TData>
    {
        TData Data { get; }
    }
}
