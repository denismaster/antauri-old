using System.Security.Cryptography;
using System.Text;

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
}
