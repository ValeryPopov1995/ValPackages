using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using ValPackage.Common.PauseSystem;
using Zenject;

namespace ValPackage.Common.Ui
{
    public class PauseMenuLoader : MonoBehaviour
    {
        [SerializeField] private AssetReference _view;
        [SerializeField] private bool _pauseOnLoad = true;
        [Inject] private Pauser _pauser;
        private bool _loaded;
        private CoreView _instance;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Load();
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private async void Load()
        {
            if (_loaded) return;

            var instance = await _view.InstantiateAsync(transform).Task;
            instance.SetActive(true);
            _instance = instance.GetComponent<CoreView>();
            _loaded = true;

            _instance.OnHide += Unload;

            if (_pauseOnLoad)
                _pauser.Pause();
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void Unload()
        {
            if (!_loaded) return;

            _view.ReleaseInstance(_instance.gameObject);
            _loaded = false;

            if (_pauseOnLoad)
                _pauser.Resume();
        }
    }
}