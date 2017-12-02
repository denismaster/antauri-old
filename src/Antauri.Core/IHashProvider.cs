using Antauri.Core;

namespace Antauri.Core
{
    /// <summary>
    /// Interface for something that has hash
    /// </summary>
    public interface IHashable
    {
        /// <summary>
        /// Object's hash
        /// </summary>
        string Hash { get; set; }

        /// <summary>
        /// Returns bytes for hash computation
        /// </summary>
        /// <returns></returns>
        byte[] GetHashData();
    }

    /// <summary>
    /// Interface for hashing and verifying IHashable's
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Computes hash from GetHashData results and set's it to the Hash Property of IHashable
        /// </summary>
        /// <param name="input">IHashable to proceed</param>
        void Hash(IHashable input);
        /// <summary>
        /// Computes hash from GetHashData results and checks the equality of result and IHashable's hash
        /// </summary>
        /// <param name="input">IHashable to proceed</param>
        /// <returns>true, if hash is correct</returns>
        bool Verify(IHashable input);
    }
}
