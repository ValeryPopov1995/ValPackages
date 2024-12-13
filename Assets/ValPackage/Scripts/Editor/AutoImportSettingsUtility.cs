using ValeryPopov.Common.Extantions;
using System;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ValeryPopov.Common.Editor
{
    /// <summary>
    /// Settings pattern to import settings of selected media
    /// </summary>
    public class AutoImportSettingsUtility : EditorWindow
    {
        private const string menuFolder = "Tools/ValPackage/Window/";

        // audio
        private bool _loadInBack;
        private int _audioQuality = 50;

        // texture
        private bool _useCrounchCompression;
        private int _crounchCompression = 50;
        private TextureImporterCompression _textureCompression;
        private bool _mipMaps;

        // model
        private bool _readWrite;
        private ModelImporterMeshCompression _meshCompression;



        [MenuItem(menuFolder + "Auto Import Sets")]
        private static void Init()
        {
            var window = (AutoImportSettingsUtility)GetWindow(typeof(AutoImportSettingsUtility));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Audio");
            _loadInBack = EditorGUILayout.Toggle("Load in background", _loadInBack);
            _audioQuality = EditorGUILayout.IntSlider("Audio quality", _audioQuality, 0, 100);
            if (GUILayout.Button("Set Audio selected"))
                SetAudio();

            GUILayout.Label("_______________________________________________________________________________________________________________________________________");
            GUILayout.Label("Textures");
            _useCrounchCompression = EditorGUILayout.Toggle("Use crounch compression", _useCrounchCompression);
            _crounchCompression = EditorGUILayout.IntSlider("Crounch quality", _crounchCompression, 0, 100);
            _textureCompression = (TextureImporterCompression)EditorGUILayout.EnumPopup("Texture compression", _textureCompression);
            _mipMaps = EditorGUILayout.Toggle("Mip Maps", _mipMaps);
            if (GUILayout.Button("Set Texture selected"))
                SetTexture();

            GUILayout.Label("_______________________________________________________________________________________________________________________________________");
            GUILayout.Label("Models");
            _readWrite = EditorGUILayout.Toggle("Read/Write", _readWrite);
            _meshCompression = (ModelImporterMeshCompression)EditorGUILayout.EnumPopup("Mesh compression", _meshCompression);
            if (GUILayout.Button("Set Models selected"))
                SetModels();
        }

        private void SetAudio()
        {
            SetSelected<AudioImporter>(importer =>
            {
                importer.loadInBackground = _loadInBack;
                var sets = importer.defaultSampleSettings;
                sets.quality = (float)_audioQuality / 100;
                importer.defaultSampleSettings = sets;
            });
        }

        private void SetTexture()
        {
            SetSelected<TextureImporter>(importer =>
            {
                importer.crunchedCompression = _useCrounchCompression;
                importer.compressionQuality = _crounchCompression;
                importer.textureCompression = _textureCompression;
                importer.mipmapEnabled = _mipMaps;
            });
        }

        private void SetModels()
        {
            SetSelected<ModelImporter>(importer =>
            {
                importer.isReadable = _readWrite;
                importer.meshCompression = _meshCompression;
            });
        }



        private async void SetSelected<T>(Action<T> foreachAction) where T : AssetImporter
        {
            var ids = Selection.assetGUIDs;
            int count = 0;

            foreach (var id in ids)
            {
                await Task.Yield();

                string path = AssetDatabase.GUIDToAssetPath(id);
                T importer = AssetImporter.GetAtPath(path) as T;

                if (importer)
                {
                    foreachAction?.Invoke(importer);
                    AssetDatabase.ImportAsset(path);
                    count++;
                }
            }

            this.Log(count + " files changed");
        }
    }
}