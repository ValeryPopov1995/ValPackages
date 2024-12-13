using UnityEditor;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Editor
{
    public class NamingUtility : EditorWindow
    {
        private const string menuFolder = "Tools/ValPackage/Window/";
        private string text = "_Rus";

        [MenuItem(menuFolder + "Naming Utility")]
        private static void Init()
        {
            var window = (NamingUtility)GetWindow(typeof(NamingUtility));
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            text = EditorGUILayout.TextField("Text Field", text);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Text after"))
            {
                Selection.objects.ForEach(obj =>
                {
                    var path = AssetDatabase.GetAssetPath(obj);
                    AssetDatabase.RenameAsset(path, obj.name + text);
                });
            }

            if (GUILayout.Button("Add Text before"))
            {
                Selection.objects.ForEach(obj =>
                {
                    var path = AssetDatabase.GetAssetPath(obj);
                    AssetDatabase.RenameAsset(path, text + obj.name);
                });
            }

            if (GUILayout.Button("Remove from name"))
            {
                Selection.objects.ForEach(obj =>
                {
                    if (obj.name.Contains(text))
                    {
                        var path = AssetDatabase.GetAssetPath(obj);
                        AssetDatabase.RenameAsset(path, obj.name.Replace(text, ""));
                    }
                });
            }

            GUILayout.EndHorizontal();
        }
    }
}