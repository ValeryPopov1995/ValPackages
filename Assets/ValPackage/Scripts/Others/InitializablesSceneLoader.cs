using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ValeryPopov.Common
{
    /// <summary>
    /// Load scene after initializables
    /// </summary>
    public class InitializablesSceneLoader : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _initializableObjects;
        private IInitializable[] _initializables;

        private async void Awake()
        {
            _initializables = _initializableObjects
                .Where(mono => mono is IInitializable)
                .Select(mono => mono as IInitializable)
                .ToArray();

            foreach (var item in _initializables)
            {
                await item.Initialize();
            }

            if (_initializables.Length > 0)
                await UniTask.WaitWhile(() => _initializables.Any(init => !init.Initialized));

            SceneManager.LoadScene(1);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _initializableObjects = _initializableObjects
                .Where(mono => mono is IInitializable)
                .ToArray();
        }
#endif
    }
}