using UnityEngine;

namespace ValeryPopov.Common.Extantions
{
    public static class TransfromExtantion
    {
        public static void SetPositionAndRotation(this Transform transform, Transform target)
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
        }
    }
}