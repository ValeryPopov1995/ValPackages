using UnityEngine;

namespace ValeryPopov.Common
{
    [System.Serializable]
    public struct CustomVector3
    {
        [Tooltip("Direction and value")]
        public Vector3 Value;
        [Tooltip("One-magnitude random direction")]
        public bool RandomValue;
        public Vector2 RandomMultiply;

        public static implicit operator Vector3(CustomVector3 customVector)
        {
            var direction = customVector.RandomValue ? Random.onUnitSphere : customVector.Value;
            var magnitude = Random.Range(customVector.RandomMultiply.x, customVector.RandomMultiply.y);
            return direction * magnitude;
        }
    }
}