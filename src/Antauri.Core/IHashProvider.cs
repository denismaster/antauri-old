using Antauri.Core;

public interface IHashProvider
{
    string Hash<T>(BlockData<T> input);
}