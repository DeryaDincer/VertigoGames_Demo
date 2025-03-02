using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertigoGames.Managers
{
    public class ObserverManager
    {
        private static Dictionary<Type, Action<object>> observers = new Dictionary<Type, Action<object>>();

        public static void Register<T>(Action<T> observer)
        {
            Type eventType = typeof(T);
            if (!observers.ContainsKey(eventType))
            {
                observers[eventType] = _ => { };
            }
            observers[eventType] += (obj) => observer((T)obj);
        }

        public static void Unregister<T>(Action<T> observer)
        {
            Type eventType = typeof(T);
            if (observers.ContainsKey(eventType))
            {
                observers[eventType] -= (obj) => observer((T)obj);
            }
        }

        public static void Notify<T>(T eventData)
        {
            Type eventType = typeof(T);
            if (observers.ContainsKey(eventType))
            {
                observers[eventType]?.Invoke(eventData);
            }
        }
    }
 
}