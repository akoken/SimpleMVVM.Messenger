using System;

namespace MessengerService.Core
{
    public interface IMessenger
    {
        /// <summary>
        /// Registers subscriber for the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Register<T>(Action<T> action);

        /// <summary>
        /// Removes subscriber with the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        void Unregister<T>(Action<T> action);

        /// <summary>
        /// Sends a message to subscribers registered with this type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Send<T>(T message);
    }
}
