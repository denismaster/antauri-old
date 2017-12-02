namespace Antauri.Core
{
    public interface IGenesisBlockFactory<TBlock> where TBlock:IBasicBlock
    {
        TBlock CreateGenesisBlock();
    }
}
