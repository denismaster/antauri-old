using System.Security.Cryptography;
using System.Text;
using Antauri.Core;

public class SHA256HashProvider : IHashProvider
{
    public void Hash(IHashable input)
    {
        var builder = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            byte[] result = hash.ComputeHash(input.GetHashData());

            foreach (byte b in result)
                builder.Append(b.ToString("x2"));
        }

        input.Hash = builder.ToString();
    }

    public bool Verify(IHashable input)
    {
        var builder = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            byte[] result = hash.ComputeHash(input.GetHashData());

            foreach (byte b in result)
                builder.Append(b.ToString("x2"));
        }

        return builder.ToString() == input.Hash;
    }
}
