using UnityEngine;
using Zenject;

namespace ValPackage.Common.Localization
{
    public abstract class LocalizationController : MonoBehaviour
    {
        [field: SerializeField] public string ID { get; protected set; } = "test_text";
        [Inject] protected LocalizationManager _localizationManager;



        protected virtual void Awake()
        {
            ID = ID.Trim();
        }

        protected void OnEnable()
        {
            if (_localizationManager)
                _localizationManager.OnLanguageChanged += SetData;
            SetData();
        }

        protected void OnDisable()
        {
            if (_localizationManager)
                _localizationManager.OnLanguageChanged -= SetData;
        }

        private void SetData(Language lan) => SetData();

        protected abstract void SetData();

#if UNITY_EDITOR
        [ContextMenu("Select LocalizationManager instance")]
        public void SelectLocalizationManager()
        {
            LocalizationManager manager = FindFirstObjectByType<LocalizationManager>();

            if (manager != null)
                UnityEditor.Selection.activeGameObject = manager.gameObject;
        }
#endif
    }
}