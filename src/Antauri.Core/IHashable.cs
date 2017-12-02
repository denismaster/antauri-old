namespace Antauri.Core
{
    public interface IHashable
    {
        string Hash { get; set; }
        byte[] GetHashData();
    }
}
