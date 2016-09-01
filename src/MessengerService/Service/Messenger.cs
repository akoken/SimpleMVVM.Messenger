using System;
using System.Collections.Generic;
using MessengerService.Core;

namespace MessengerService.Service
{
    /// <summary>
    /// Manages communication between viewmodels
    /// </summary>
    public class Messenger : IMessenger
    {
        private static Dictionary<Type, List<object>> subscribers = new Dictionary<Type, List<object>>();
        private static readonly Messenger MessengerService = new Messenger();
        private object _lock = new object();

        public static Messenger Default
        {
            get
            {
                return MessengerService;
            }
        }

        private Messenger() { }

        public void Register<T>(Action<T> action)
        {
            lock (_lock)
            {
                if (subscribers.ContainsKey(typeof(T)))
                {
                    var actions = subscribers[typeof(T)];
                    actions.Add(action);
                }
                else
                {
                    var actions = new List<object> { action };
                    subscribers.Add(typeof(T), actions);
                }
            }
        }
        public void Unregister<T>(Action<T> action)
        {
            lock (_lock)
            {
                if (!subscribers.ContainsKey(typeof(T))) return;

                var actions = subscribers[typeof(T)];
                actions.Remove(action);
                if (actions.Count == 0)
                {
                    subscribers.Remove(typeof(T));
                }
            }
        }
        public void Send<T>(T message)
        {
            if (!subscribers.ContainsKey(typeof(T))) return;

            var actions = subscribers[typeof(T)];
            foreach (Action<T> action in actions)
            {
                action.Invoke(message);
            }
        }
    }
}
