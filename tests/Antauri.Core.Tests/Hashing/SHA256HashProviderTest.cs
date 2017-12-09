using Xunit;

namespace Antauri.Core.Tests.Hashing
{
    public class SHA256HashProviderTest
    {
        [Fact]
        public void HashTest()
        {
            var block = new SimpleBlock(2, "2", 0, "new block");

            var hashProvider = new SHA256HashProvider();

            hashProvider.Hash(block);

            Assert.Equal(block.Hash, "b77f43514bcd6c8c963b94c0bb66cbd6da8f5606599f9a32581d22368df02a81");
        }

        [Fact]
        public void VerifyTest()
        {
            var blockWithValidHash = new SimpleBlock(2, "2", 0, "new block", "b77f43514bcd6c8c963b94c0bb66cbd6da8f5606599f9a32581d22368df02a81");
            var blockWithInvalidHash = new SimpleBlock(2, "2", 0, "new block", "b22f43514bcd6c8c963b94c0bb66cbd6da8f5606599f9a32581d22322df02a22");

            var hashProvider = new SHA256HashProvider();

            var validBlockResult = hashProvider.Verify(blockWithValidHash);
            var invalidBlockResult = hashProvider.Verify(blockWithInvalidHash);

            Assert.True(validBlockResult);
            Assert.False(invalidBlockResult);
        }
    }
}
