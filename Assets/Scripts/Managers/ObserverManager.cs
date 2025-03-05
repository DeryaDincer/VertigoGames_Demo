using System;
using System.Collections.Generic;

namespace VertigoGames.Managers
{
    public class ObserverManager
    {
        private static readonly Dictionary<Type, Action<object>> _observers = new Dictionary<Type, Action<object>>();

        public static void Register<T>(Action<T> observer)
        {
            Type eventType = typeof(T);

            if (!_observers.ContainsKey(eventType))
            {
                _observers[eventType] = _ => { };
            }

            _observers[eventType] += obj => observer((T)obj);
        }

        public static void Unregister<T>(Action<T> observer)
        {
            Type eventType = typeof(T);

            if (_observers.ContainsKey(eventType))
            {
                _observers[eventType] -= obj => observer((T)obj);
            }
        }

        public static void Notify<T>(T eventData)
        {
            Type eventType = typeof(T);

            if (_observers.ContainsKey(eventType))
            {
                _observers[eventType]?.Invoke(eventData);
            }
        }
    }
}