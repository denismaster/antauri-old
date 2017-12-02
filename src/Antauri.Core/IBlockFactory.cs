namespace Antauri.Core
{
    public interface IBlockFactory<TBlock,TData> where TBlock : IBasicBlock, IHasData<TData>
    {
        TBlock CreateBlock(TBlock lastBlock, TData data);
    }
}
