using Xunit;

namespace Antauri.Core.Tests
{
    public class BlockTest
    {
        [Fact]
        public void GenesisBlockTest()
        {
            IHashProvider hashProvider = new SHA256HashProvider();
            IGenesisBlockFactory<SimpleBlock> blockFactory = new SimpleBlockFactory(hashProvider);

            var genesisBlock = blockFactory.CreateGenesisBlock();

            Assert.True(hashProvider.Verify(genesisBlock));
        }
    }
}
