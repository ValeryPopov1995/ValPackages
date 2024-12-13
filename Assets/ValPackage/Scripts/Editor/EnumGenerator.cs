using NaughtyAttributes;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ValeryPopov.Common.Editor
{
    [CreateAssetMenu(menuName = "Scriptable/ValeryCommon/Enum Generator")]
    public class EnumGenerator : ScriptableObject
    {
        [SerializeField] private string EnumName = "SaveKeysType";
        [SerializeField] private string Namespace = "ValeryPopov.Game";
        [SerializeField] private string[] _keys;

        [Button]
        private void GenerateSaveKeys()
        {
            var fildPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var directory = fildPath.Substring(0, fildPath.LastIndexOf('/'));
            var newFilePath = $"{directory}/{EnumName}.cs";

            var writer = File.CreateText(newFilePath);
            writer.Write(GenerateScript());
            writer.Close();
        }

        private string GenerateScript()
        {
            string data = "";
            bool useNamespace = Namespace != "";

            if (useNamespace)
                data = "namespace " + Namespace + "\n{\n\t";

            data += "public enum " + EnumName + "\n" + (useNamespace ? "\t{\n" : "{\n");

            foreach (var key in _keys)
            {
                if (useNamespace) data += "\t";
                data += "\t" + key + ",\n";
            }

            if (useNamespace) data += "\t";
            data += "}";

            if (useNamespace) data += "\n}";

            return data;
        }
    }
}