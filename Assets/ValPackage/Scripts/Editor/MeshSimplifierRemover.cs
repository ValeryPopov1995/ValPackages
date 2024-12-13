using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityMeshSimplifier;

namespace ValeryPopov.Common.Editor
{
    public static class MeshSimplifierRemover
    {
        [MenuItem("Tools/ValPackage/Remove Mesh Simplifier Components #%-")]
        public static void RemoveMeshSimplifierComponents() => RemoveMeshSimplifierComponents(Selection.activeGameObject);

        /// <summary>
        /// Removes all MeshSimplifier components from the given game object. Warning: Undo not recorded
        /// </summary>
        /// <param name="gameObject">scene object or prefab</param>
        public static void RemoveMeshSimplifierComponents(GameObject gameObject)
        {
            if (gameObject is null)
            {
                Debug.LogWarning("GameObject is null to remove MeshSimplifier components");
                return;
            }

            // LOD helpers
            Object.DestroyImmediate(gameObject.GetComponent<LODGeneratorHelper>(), true);
            Object.DestroyImmediate(gameObject.GetComponent("LODBackupComponent"), true);

            // root disabled mesh
            Object.DestroyImmediate(gameObject.GetComponent<MeshRenderer>(), true);
            Object.DestroyImmediate(gameObject.GetComponent<MeshFilter>(), true);

            // children disabled meshes
            var oldRenders = gameObject.GetComponentsInChildren<MeshRenderer>(true)
                .Where(mr => !mr.enabled);
            foreach (var render in oldRenders)
                Object.DestroyImmediate(render.gameObject, true);

            Debug.Log("MeshSimplifier components removed", gameObject);
        }
    }
}