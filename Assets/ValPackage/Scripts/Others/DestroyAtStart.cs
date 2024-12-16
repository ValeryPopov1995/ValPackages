using NaughtyAttributes;
using UnityEngine;

namespace ValPackage.Common
{
    public class DestroyAtStart : MonoBehaviour
    {
        public enum DestroyMode
        {
            DestroyChance,
            StayOnlyOne
        }

        [SerializeField] private DestroyMode _mode = DestroyMode.DestroyChance;
        [SerializeField, ShowIf("_mode", DestroyMode.DestroyChance), Range(0, 10)] private int _stayChance = 5;
        [SerializeField, ShowIf("_mode", DestroyMode.StayOnlyOne)] private GameObject[] _destroyObjects;

        private void Start()
        {
            switch (_mode)
            {
                case DestroyMode.DestroyChance:
                    if (Random.Range(0, 10) > _stayChance)
                    {
                        Destroy(gameObject);
                        return;
                    }
                    break;
                case DestroyMode.StayOnlyOne:
                    int stayIndex = Random.Range(0, _destroyObjects.Length);
                    for (int i = 0; i < _destroyObjects.Length; i++)
                    {
                        if (i == stayIndex) continue;
                        Destroy(_destroyObjects[i]);
                    }
                    break;
            }

            Destroy(this);
        }
    }
}