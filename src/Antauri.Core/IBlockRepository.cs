using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Antauri.Core
{
    public interface IBlockRepository<TBlock>
        where TBlock: IBasicBlock
    {
        Task<IEnumerable<TBlock>> GetBlocksAsync();
        Task SaveBlocksAsync();
    }
}
