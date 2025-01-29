using UnityEngine;

namespace ValPackage.Common.Extensions
{
    public static class TransfromExtensions
    {
        public static void SetPositionAndRotation(this Transform transform, Transform target)
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
        }
    }
}