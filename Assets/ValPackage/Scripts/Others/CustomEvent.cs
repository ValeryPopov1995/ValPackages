using System;
using UnityEngine.Events;

namespace ValPackage.Common
{
    [Serializable]
    public struct CustomEvent
    {
        public event Action Action;
        public UnityEvent UnityEvent;

        public static CustomEvent operator +(CustomEvent customEvent, Action action)
        {
            customEvent.Action += action;
            return customEvent;
        }
        public static CustomEvent operator -(CustomEvent customEvent, Action action)
        {
            customEvent.Action -= action;
            return customEvent;
        }
        public void TryInvoke()
        {
            Action?.Invoke();
            UnityEvent?.Invoke();
        }
    }

    [Serializable]
    public struct CustomEvent<T>
    {
        public event Action<T> Action;
        public UnityEvent<T> UnityEvent;

        public static CustomEvent<T> operator +(CustomEvent<T> customEvent, Action<T> action)
        {
            customEvent.Action += action;
            return customEvent;
        }
        public static CustomEvent<T> operator -(CustomEvent<T> customEvent, Action<T> action)
        {
            customEvent.Action -= action;
            return customEvent;
        }
        public void TryInvoke(T T)
        {
            Action?.Invoke(T);
            UnityEvent?.Invoke(T);
        }
    }

    [Serializable]
    public struct CustomEvent<T1, T2>
    {
        public event Action<T1, T2> Action;
        public UnityEvent<T1, T2> UnityEvent;

        public static CustomEvent<T1, T2> operator +(CustomEvent<T1, T2> customEvent, Action<T1, T2> action)
        {
            customEvent.Action += action;
            return customEvent;
        }
        public static CustomEvent<T1, T2> operator -(CustomEvent<T1, T2> customEvent, Action<T1, T2> action)
        {
            customEvent.Action -= action;
            return customEvent;
        }
        public void TryInvoke(T1 T1, T2 T2)
        {
            Action?.Invoke(T1, T2);
            UnityEvent?.Invoke(T1, T2);
        }
    }
}