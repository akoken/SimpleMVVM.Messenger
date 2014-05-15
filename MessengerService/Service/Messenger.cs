using System;
using System.Collections.Generic;
using MessengerService.Core;

namespace MessengerService.Service
{
    /// <summary>
    /// Provides communication between viewmodels
    /// </summary>
    public class Messenger : IMessenger
    {        
        #region Fields
        private static Dictionary<Type, List<object>> subscribers = new Dictionary<Type, List<object>>();
        private static readonly Messenger MessengerService = new Messenger();
        private object sync = new object();
        #endregion

        #region Properties
        public static Messenger Default
        {
            get
            {
                return MessengerService;
            }
        }
        #endregion

        #region Constructor
        private Messenger() { }
        #endregion

        #region Implementation of IMessenger

        public void Register<T>(Action<T> action)
        {
            lock (sync)
            {
                if (subscribers.ContainsKey(typeof(T)))
                {
                    var actions = subscribers[typeof(T)];
                    actions.Add(action);
                }
                else
                {
                    var actions = new List<object> {action};
                    subscribers.Add(typeof(T), actions);
                }
            }
        }
        public void Unregister<T>(Action<T> action)
        {
            lock (sync)
            {
                if (!subscribers.ContainsKey(typeof (T))) return;

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
            if (!subscribers.ContainsKey(typeof (T))) return;

            var actions = subscribers[typeof(T)];
            foreach (Action<T> action in actions)
            {
                action.Invoke(message);
            }
        }        

        #endregion
    }
}
