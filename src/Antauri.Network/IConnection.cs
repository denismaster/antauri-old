using System;

namespace Antauri.Network
{
    public interface IConnection
    {
        IObservable<Message> Message { get; }
    }
}
