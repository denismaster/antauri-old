using Antauri.Core;

namespace Antauri.Core
{
    public interface IHashable
    {
        string Hash { get; set; }
        byte[] GetHashData();
    }
    public interface IHashProvider
    {
        void Hash(IHashable input);
        bool Verify(IHashable input);
    }
}
