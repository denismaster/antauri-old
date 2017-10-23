using System.Security.Cryptography;
using System.Text;
using Antauri.Core;

public class SHA256HashProvider : IHashProvider
{
    public string Hash(string input)
    {
        var builder = new StringBuilder();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(input));

            foreach (byte b in result)
                builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }

    public string Hash<T>(BlockData<T> blockData)
    {
        var builder = new StringBuilder();

        var input = builder
        .Append(blockData.PreviousHash)
        .Append(blockData.TimeStamp)
        .Append(blockData.Data)
        .ToString();

        builder.Clear();

        using (var hash = SHA256.Create())
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(input));

            foreach (byte b in result)
                builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
