using System;
using UnityEngine;
using ValeryPopov.Common.SaveLoad;
using Zenject;

namespace ValeryPopov.Common.Ui
{
    /// <summary>
    /// Save value to <see cref="SaveSystem"/>
    /// </summary>
    public abstract class SavedUiComponent : MonoBehaviour
    {
        public Action OnUiValueChanged;
        [SerializeField] protected SaveFileType _saveFileType = SaveFileType.SettingsPreferences;
        [SerializeField] protected string _saveKey = "save_key";
        [SerializeField] protected bool _saveOnChanged = true;
        [Inject] protected SaveSystem _saveSystem;

        protected virtual void OnEnable()
        {
            Load();
        }

        /// <summary>
        /// Load to visual part of UiComponent from SaveSystem
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Save from UiComponent to SaveSystem
        /// </summary>
        public abstract void Save();
    }
}