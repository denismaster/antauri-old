using Xunit;

namespace Antauri.Core.Tests
{
    public class BlockChainTest
    {
        [Fact]
        public void IsValidNewBlockTest()
        {
            var hasher = new SHA256HashProvider();
            var factory = new SimpleBlockFactory(hasher);

            var blockchain = new BlockChain(hasher, factory);

            var genesisBlock = factory.CreateGenesisBlock();

            var blockWithBadIndex = new SimpleBlock(0,"1",0,"new block","0");
            Assert.False(blockchain.IsValidNewBlock(genesisBlock, blockWithBadIndex));

            var blockWithBadHash = new SimpleBlock(1,"1",0,"new block","0");
            Assert.False(blockchain.IsValidNewBlock(genesisBlock, blockWithBadHash));
        }
    }
}
