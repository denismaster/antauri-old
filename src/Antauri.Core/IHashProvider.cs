using Antauri.Core;

namespace Antauri.Core
{
    public interface IHashProvider
    {
        void Hash(IHashable input);
        bool Verify(IHashable input);
    }
}
