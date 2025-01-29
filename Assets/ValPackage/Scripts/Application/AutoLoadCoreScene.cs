using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using ValPackage.Common.Extensions;
using Zenject;

namespace ValPackage.Common.App
{
    [DefaultExecutionOrder(-10000)]
    public class AutoLoadCoreScene : MonoBehaviour
    {
        private void Awake()
        {
            if (!ProjectContext.Instance)
            {
                this.LogWarning("no project context, loading first scene and clearing log");
                SceneManager.LoadScene(0);
#if UNITY_EDITOR
                ClearLog();
#endif
            }
            else
                Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }
#endif
    }
}