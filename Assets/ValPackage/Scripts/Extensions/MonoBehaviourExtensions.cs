using UnityEngine;

namespace ValPackage.Common.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static bool IsAsyncEnabled(this MonoBehaviour monoBehaviour)
        {
            return monoBehaviour != null && monoBehaviour.enabled;
        }
    }
}