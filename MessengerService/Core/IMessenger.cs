using System;

namespace MessengerService.Core
{
    public interface IMessenger
    {
        void Register<T>(Action<T> action);
        void Unregister<T>(Action<T> action);
        void Send<T>(T message);
    }
}
