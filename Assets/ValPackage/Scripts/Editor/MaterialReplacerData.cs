using System.Collections.Generic;
using UnityEngine;

namespace ValPackage.Common.Editor
{
    [CreateAssetMenu(menuName = "Scriptable/ValPackage/MaterialReplacer Data")]
    public class MaterialReplacerData : ScriptableObject
    {
        public List<Material> OldMaterials;
        public List<Material> NewMaterials;
    }
}