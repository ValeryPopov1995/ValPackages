using UnityEngine;

namespace ValPackage.Common
{
    public class DestroyTimer : MonoBehaviour
    {
        [field: SerializeField] public bool DestroyAtStart { get; set; } = true;
        [field: SerializeField] public float Timer { get; set; } = 10;



        void Start()
        {
            if (DestroyAtStart)
                DestroyByTimer();
        }

        private void DestroyByTimer(float timer = -1)
        {
            if (timer < 0)
                timer = Timer;

            DoBeforeDestroy();
            Destroy(gameObject, timer);
        }

        protected virtual void DoBeforeDestroy() { }
    }
}