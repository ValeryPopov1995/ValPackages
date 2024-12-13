using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValeryPopov.Common
{
    /// <summary>
    /// Pool of T instances
    /// </summary>
    /// <typeparam name="T">Prefab class</typeparam>
    public abstract class ObjectPool<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected T _prefab;
        [SerializeField, Min(0)] protected int _maxCount = 0;
        protected List<T> _pool = new();

        /// <returns>Free pool instance</returns>
        protected T GetFreeInstance()
        {
            var instance = _pool.FirstOrDefault(t => IsFreeInstance(t));
            bool canInstantiate = !instance && _maxCount == 0 || (_maxCount > 0 && _pool.Count < _maxCount);

            if (canInstantiate)
            {
                instance = Instantiate(_prefab, transform);
                _pool.Add(instance);
            }

            return instance;
        }

        /// <param name="instance">Free pool instance</param>
        /// <returns>True if has free instance</returns>
        protected bool GetFreeInstance(out T instance)
        {
            instance = GetFreeInstance();
            return instance != null;
        }

        protected abstract bool IsFreeInstance(T instance);
    }
}