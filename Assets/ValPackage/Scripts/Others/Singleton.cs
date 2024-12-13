using UnityEngine;

namespace ValeryPopov.Common
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        [SerializeField] private bool _dontDestroy = false;

        protected virtual void Awake()
        {
            Instance = GetComponent<T>();
            if (_dontDestroy) DontDestroyOnLoad(gameObject);
        }
    }
}