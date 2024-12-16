using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ValPackage.Common.Editor
{
    /// <summary>
    /// Usefull for perlace gameObject from one empty object to another (empty object is like an folder)
    /// </summary>
    public class Parenter : EditorWindow
    {
        public Transform NewParent;

        [MenuItem("Tools/ValPackage/Window/Parenter")]
        public static void ShowWindow()
        {
            Parenter wnd = GetWindow<Parenter>();
            wnd.titleContent = new GUIContent("Parenter");
        }

        private void OnGUI()
        {
            ScriptableObject target = this;
            SerializedObject so = new SerializedObject(target);
            SerializedProperty property = so.FindProperty("NewParent");
            EditorGUILayout.PropertyField(property, true);
            so.ApplyModifiedProperties();

            if (GUILayout.Button("Set Parent for " + Selection.objects.Length))
                SetParent();
        }

        private async void SetParent()
        {
            if (NewParent == null) return;

            foreach (var obj in Selection.gameObjects)
            {
                obj.transform.SetParent(NewParent);
                EditorUtility.SetDirty(obj);
                await Task.Yield();
            }
        }
    }
}