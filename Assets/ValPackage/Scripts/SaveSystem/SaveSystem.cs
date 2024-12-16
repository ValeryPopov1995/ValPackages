using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using ValPackage.Common.Extantions;

namespace ValPackage.Common.SaveLoad
{
    /// <summary>
    /// Save values like Prefs but in text files by category
    /// </summary>
    public class SaveSystem
    {
        private const string _keyValueSeparator = "<key>";

        public event Action<SaveFileType> OnSave;

        private Dictionary<SaveFileType, Dictionary<string, string>> _saveDatas = new();



        public SaveSystem()
        {
            var saveTypes = Enum.GetValues(typeof(SaveFileType));
            foreach (var saveType in saveTypes)
                TryReadFile((SaveFileType)saveType);
        }


        public void Save(SaveFileType saveFileType, object key, object value)
        {
            Save(saveFileType, key.ToString(), value.ToString());
        }

        public void Save(SaveFileType saveFileType, string key, string value)
        {
            if (_saveDatas[saveFileType].ContainsKey(key))
                _saveDatas[saveFileType][key] = value;
            else
                _saveDatas[saveFileType].Add(key, value);

            SaveFile(saveFileType);
        }

        public string Load(SaveFileType saveFileType, object key)
        {
            return LoadString(saveFileType, key.ToString());
        }

        public bool HasKey(SaveFileType saveFileType, object key)
        {
            return _saveDatas[saveFileType].ContainsKey(key.ToString());
        }



        private void TryReadFile(SaveFileType saveFileType)
        {
            _saveDatas.Add(saveFileType, new());
            var saveData = _saveDatas[saveFileType];
            string filePath = GetFilePath(saveFileType);
            if (!File.Exists(filePath))
            {
                var stream = File.Create(filePath);
                stream.Close();
                return;
            }
            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                if (string.Empty == line) continue;

                var key = line.Substring(0, line.IndexOf(_keyValueSeparator));
                var value = line.Substring(line.IndexOf(_keyValueSeparator) + _keyValueSeparator.Length);
                saveData.Add(key, value);
            }
        }

        private void SaveFile(SaveFileType saveFileType)
        {
            string filePath = GetFilePath(saveFileType);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            var lines = _saveDatas[saveFileType]
                .Select(saveData => saveData.Key + _keyValueSeparator + saveData.Value);
            File.WriteAllLines(filePath, lines);

            OnSave?.Invoke(saveFileType);
        }

        private string GetFilePath(SaveFileType saveFileType)
        {
#if UNITY_EDITOR
            string savesDirectory = Application.dataPath + "/Saves/";
#else
            string savesDirectory = Application.persistentDataPath + "/"; // Important. No additional folders
#endif

            if (!Directory.Exists(savesDirectory))
            {
                Directory.CreateDirectory(savesDirectory);
                this.Log("Saves directory created");
            }

            return savesDirectory + saveFileType + ".txt";
        }



        public string LoadString(SaveFileType saveFileType, string key, string defaultValue = "")
        {
            try
            {
                return _saveDatas[saveFileType][key];
            }
            catch (Exception)
            {
                this.Log($"key \'{key}\' not loaded, returned default value = {defaultValue}");
                return defaultValue;
            }
        }

        public bool LoadBool(SaveFileType saveFileType, string key, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(LoadString(saveFileType, key));
            }
            catch
            {
                this.Log($"{key} not loaded, returned default value = {defaultValue}");
                return defaultValue;
            }
        }

        public int LoadInt(SaveFileType saveFileType, string key, int defaultValue = 0)
        {
            try
            {
                return int.Parse(LoadString(saveFileType, key));
            }
            catch
            {
                this.Log($"{key} not loaded, returned default value = {defaultValue}");
                return defaultValue;
            }
        }

        public float LoadFloat(SaveFileType saveFileType, string key, float defaultValue = 0)
        {
            try
            {
                return float.Parse(LoadString(saveFileType, key));
            }
            catch
            {
                this.Log($"{key} not loaded, returned default value = {defaultValue}");
                return defaultValue;
            }
        }
    }
}