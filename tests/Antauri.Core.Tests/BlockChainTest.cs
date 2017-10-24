using Xunit;

namespace Antauri.Core.Tests
{
    public class BlockChainTest
    {
        [Fact]
        public void IsValidNewBlockTest()
        {
            var hasher = new SHA256HashProvider();
            var blockchain = new BlockChain(hasher);

            var genesisBlock = Block.GenesisBlock;

            var blockWithBadIndex = new Block(0,"1",0,"new block","0");
            Assert.False(blockchain.IsValidNewBlock(genesisBlock, blockWithBadIndex));

            var blockWithBadHash = new Block(1,"1",0,"new block","0");
            Assert.False(blockchain.IsValidNewBlock(genesisBlock, blockWithBadHash));
        }
    }
}
