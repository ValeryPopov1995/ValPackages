using System.Collections.Generic;
using UnityEngine;

namespace ValeryPopov.Common.Editor
{
    [CreateAssetMenu(menuName = "Scriptable/ValeryCommon/MaterialReplacer Data")]
    public class MaterialReplacerData : ScriptableObject
    {
        public List<Material> OldMaterials;
        public List<Material> NewMaterials;
    }
}