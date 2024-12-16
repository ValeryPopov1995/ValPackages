using UnityEngine;

namespace ValPackage.Common.Animation
{
    [RequireComponent(typeof(Animator))]
    public class RandomAnimationSpeed : MonoBehaviour
    {
        [SerializeField] private Vector2 _randomRange = new(.8f, 1.2f);

        private void Start()
        {
            GetComponent<Animator>().speed = Random.Range(_randomRange.x, _randomRange.y);
        }
    }
}