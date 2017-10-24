using Xunit;

namespace Antauri.Core.Tests
{
    public class BlockTest
    {
        [Fact]
        public void GenesisBlockTest()
        {
            var block = Block.GenesisBlock;

            var blockData = new BlockData<string>()
            {
                Data = block.Data,
                PreviousHash = block.PreviousHash,
                Index = block.Index,
                TimeStamp = block.TimeStamp
            };

            IHashProvider hashProvider = new SHA256HashProvider();

            string hash = hashProvider.Hash(blockData);

            Assert.Equal(hash, block.Hash);
        }
    }
}
