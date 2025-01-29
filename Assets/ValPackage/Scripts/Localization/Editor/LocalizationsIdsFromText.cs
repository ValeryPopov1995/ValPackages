using System.Linq;
using UnityEditor;
using UnityEngine;
using ValPackage.Common.Extensions;

namespace ValPackage.Common.Localization.Editor
{
    public static class LocalizationsIdsFromText
    {
        [MenuItem("Tools/ValPackage/Localization/Get Localizations Ids from scene")]
        public static void GetLocalizationsIdsFromTextsInScene()
        {
            var ids = Object.FindObjectsByType<LocalizationController>(FindObjectsSortMode.None).Select(controller => controller.ID);
            string message = "";
            ids.ForEach(id => message += id + "\n");
            if (EditorUtility.DisplayDialog("ids in clipboard", message, "copy to buffer", "ok"))
                GUIUtility.systemCopyBuffer = message;
        }
    }
}