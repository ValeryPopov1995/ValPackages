using UnityEditor;
using UnityEngine;

namespace ValeryPopov.Common.Editor
{
    public class CopyComponentUtility : MonoBehaviour
    {
        private const string menuFolder = "Tools/ValPackage/Select/";
        private static Transform cachedTransform;

        [MenuItem(menuFolder + "Copy Transform local properties %#m")] // ctrl shift m
        public static void CopySelectedTransformProperties()
        {
            cachedTransform = Selection.activeGameObject.transform;
        }

        [MenuItem(menuFolder + "Paste Transform local properties %#j")] // ctrl shift j
        public static void PasteTransformPropertiesToSelected()
        {
            Transform selection = Selection.activeGameObject.transform;

            PasreTransformProperties(cachedTransform, selection);
        }

        private static void PasreTransformProperties(Transform from, Transform to)
        {
            Undo.RecordObject(to, "Undo paste transform properties");
            to.localPosition = from.localPosition;
            to.localRotation = from.localRotation;
            to.localScale = from.localScale;
        }
    }
}