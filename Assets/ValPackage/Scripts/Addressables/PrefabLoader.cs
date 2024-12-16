using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ValPackage.Common.Extantions;

namespace ValPackage.Common.Addressables
{
    /// <summary>
    /// Load/unload prefab to instantiate
    /// </summary>
    [Serializable]
    public class PrefabLoader<T> where T : Component
    {
        public bool Loaded => Prefab != null;

        [SerializeField] private AssetReference _prefab;
        public T Prefab { get; private set; }

        public async Task Load()
        {
            if (!CheckAsset()) return;
            if (Loaded) return;
            Prefab = (await _prefab.LoadAssetAsync<GameObject>().Task).GetComponent<T>();
        }

        public void Release()
        {
            Prefab = null;
            _prefab.ReleaseAsset();
        }

        /// <summary>
        /// Load() not required
        /// </summary>
        /// <returns>Instance</returns>
        public async Task<T> InstantiateAsync(Transform parent = null)
        {
            if (CheckAsset())
                return (await _prefab.InstantiateAsync(parent).Task).GetComponent<T>();
            else
                return null;
        }

        /// <summary>
        /// Load() not required
        /// </summary>
        /// <returns>Instance</returns>
        public async Task<T> InstaniateAsync(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (CheckAsset())
                return (await _prefab.InstantiateAsync(position, rotation, parent).Task).GetComponent<T>();
            else
                return null;
        }

        private bool CheckAsset()
        {
            if (_prefab.AssetGUID == string.Empty)
            {
                this.LogError("no reference");
                return false;
            }

            return true;
        }


        /// <summary>
        /// Load() required
        /// </summary>
        /// <returns>Instance</returns>
        public async Task<T> Instantiate(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            if (CheckAsset())
            {
                if (!Loaded) await Load();
                return GameObject.Instantiate(Prefab, position, rotation, parent);
            }
            else
                return null;
        }

        /// <summary>
        /// Load() required
        /// </summary>
        /// <returns>Instance</returns>
        public async Task<T> Instantiate(Transform parent = null)
        {
            if (CheckAsset())
            {
                if (!Loaded) await Load();
                return GameObject.Instantiate(Prefab, parent);
            }
            else
                return null;
        }
    }
}