using Xunit;

namespace Antauri.Core.Tests
{
    public class BlockTest
    {
        [Fact]
        public void GenesisBlockTest()
        {
            IHashProvider hashProvider = new SHA256HashProvider();
            IBlockFactory<string> blockFactory = new SimpleBlockFactory<string>(hashProvider);

            var genesisBlock = blockFactory.CreateGenesisBlock();

            Assert.True(hashProvider.Verify(genesisBlock));
        }
    }
}
