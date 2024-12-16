using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using ValPackage.Common.Extantions;
using ValPackage.Common.Rendering;
using Zenject;

namespace ValPackage.Common.Addressables
{
    /// <summary>
    /// Загружает/выгружает сцену с помощью Addressables
    /// </summary>
    public class SceneAddressableLoader : MonoBehaviour
    {
        public event Action OnSceneLoaded;
        public int CurrentSceneIndex => SceneReferences.IndexOf(CurrentReference);
        [SerializeField] private AssetReference _reloadSceneReference;
        [field: SerializeField] public List<AssetReference> SceneReferences { get; private set; } = new();
        public AssetReference CurrentReference { get; private set; } = null;
        public SceneInstance CurrentSceneInstance { get; private set; }

        private bool _isBusy = false;
        [Inject] private ScreenFader _fader;



        public async Task LoadNextScene(bool fadeBeforeLoad = true, bool unfadeAfterLoad = true)
        {
            await LoadScene(CurrentSceneIndex + 1, fadeBeforeLoad, unfadeAfterLoad);
        }

        public Task ReloadScene() => LoadScene(CurrentSceneIndex, false);

        /// <summary>
        /// Загрузить/перезагрузить сцену
        /// </summary>
        /// <param name="listIndex">номер сцены в списке</param>
        /// <param name="fadeBeforeLoad">Затухание перед загрузкой сцены</param>
        /// <param name="unfadeAfterLoad">Выход из затухания после загрузки перед началом игры</param>
        public async Task LoadScene(int listIndex, bool fadeBeforeLoad = true, bool unfadeAfterLoad = true)
        {
            if (listIndex >= SceneReferences.Count || listIndex < 0)
            {
                this.LogError($"not correct level index");
                return;
            }

            _isBusy = true;

            if (fadeBeforeLoad)
                await _fader?.Fade();

            if (CurrentReference == SceneReferences[listIndex])
                await _reloadSceneReference.LoadSceneAsync().Task;

            CurrentReference = SceneReferences[listIndex];

            CurrentSceneInstance = await CurrentReference.LoadSceneAsync().Task;

            _isBusy = false;
            OnSceneLoaded?.Invoke();
            _ = Resources.UnloadUnusedAssets();

            if (unfadeAfterLoad)
                _fader?.Unfade();
        }

        public async void Quit()
        {
            if (_isBusy) return;
            _isBusy = true;

            await _fader?.Fade();

            _isBusy = false;
            Application.Quit();
        }
    }
}