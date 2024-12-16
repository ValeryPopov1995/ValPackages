using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ValPackage.Common.Editor
{
    public static class ServiceableChecker
    {
        private const string pref = "[Serviceable]";

        [MenuItem("Tools/ValPackage/Check IServiceable in Scene")]
        public static async void CheckServiceable()
        {
            var monobehs = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .Where(m => m is IServiceable);

            Debug.Log($"{pref} was found {monobehs.Count()} IServiceable Monobehaviours");

            bool notFound = true;
            foreach (var monobeh in monobehs)
            {
                var log = (monobeh as IServiceable).CheckServiceable();
                if (log != "")
                {
                    Debug.LogError($"{pref} {monobeh} : {log}");
                    notFound = false;
                }

                await UniTask.Yield();
            }

            if (notFound)
                Debug.Log(pref + " incorrect IServiceables not found");
        }
    }
}