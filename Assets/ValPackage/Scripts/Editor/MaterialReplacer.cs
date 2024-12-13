using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace ValeryPopov.Common.Editor
{

    public class MaterialReplacer : EditorWindow
    {
        public MaterialReplacerData _data;
        public List<Material> _oldMaterials => _data.OldMaterials;
        public List<Material> _newMaterials => _data.NewMaterials;



        [MenuItem("Tools/ValPackage/Window/Material Replacer")]
        public static void ShowWindow()
        {
            MaterialReplacer wnd = GetWindow<MaterialReplacer>();
            wnd.titleContent = new GUIContent("Material Replacer");
        }

        private void OnGUI()
        {
            ScriptableObject target = this;

            SerializedObject data = new SerializedObject(target);
            SerializedProperty propertyData = data.FindProperty("_data");
            EditorGUILayout.PropertyField(propertyData, true);
            data.ApplyModifiedProperties();

            if (GUILayout.Button("Try replace on slected"))
                TryReplace();
        }

        private async void TryReplace()
        {
            var renders = Selection.gameObjects
                .SelectMany(obj => obj.GetComponentsInChildren<Renderer>())
                .Distinct()
                .Where(render => render && _oldMaterials.Except(render.sharedMaterials).Count() > 0);
            var newMateralsNames = _newMaterials.Select(m => m.name);

            foreach (var render in renders)
            {
                var materials = render.sharedMaterials.ToList();
                for (int i = 0; i < materials.Count; i++)
                {
                    if (newMateralsNames.Contains(render.sharedMaterials[i].name))
                        materials[i] = _newMaterials.First(m => m.name == materials[i].name);
                    render.SetSharedMaterials(materials);
                    EditorUtility.SetDirty(render);
                }

                await Task.Yield();
            }
        }
    }
}