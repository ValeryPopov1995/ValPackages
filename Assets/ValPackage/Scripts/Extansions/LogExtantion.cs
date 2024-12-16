using UnityEngine;

namespace ValPackage.Common.Extantions
{
    public static class LogExtantion
    {
        public static void Log<T>(this T debugClass, object message)
        {
            Debug.Log($"[log][{debugClass.GetType().Name}] {message}");
        }

        public static void LogWarning<T>(this T debugClass, object message)
        {
            Debug.LogWarning($"[log][{debugClass.GetType().Name}] {message}");
        }

        public static void LogError<T>(this T debugClass, object message)
        {
            Debug.LogError($"[log][{debugClass.GetType().Name}] {message}");
        }
    }
}