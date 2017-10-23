using Antauri.Core;

public interface IHashProvider
{
    string Hash(string input);
    string Hash<T>(BlockData<T> input);
}