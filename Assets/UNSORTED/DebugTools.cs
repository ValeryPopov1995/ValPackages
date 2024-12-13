using IngameDebugConsole;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace CraneRcs
{
    public class DebugTools : MonoBehaviour
    {
        [SerializeField] private GameObject debugTools;
        private static bool inited;

        private void Awake()
        {
            if (inited)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
            inited = true;

            DebugLogConsole.AddCommand("System_UnloadUnusedAssets", "", () => Resources.UnloadUnusedAssets());
            DebugLogConsole.AddCommand("System_ClearCache", "", () => Caching.ClearCache());
            DebugLogConsole.AddCommand("System_GarbageCollect", "", () => System.GC.Collect());

            DebugLogConsole.AddCommand<int>("System_LoadScene", "", index => SceneManager.LoadSceneAsync(index));

            debugTools.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                debugTools.SetActive(!debugTools.activeSelf);
        }

        private bool Find<T>(out T component) where T : MonoBehaviour
        {
            component = FindFirstObjectByType<T>();

            if (component == null)
            {
                Debug.LogWarning("No component to use in Debug Tools");
                return false;
            }

            return true;
        }
    }
}