using IngameDebugConsole;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ValPackage.Customized
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

            DebugLogConsole.AddCommand("System_UnloadUnusedAssets", "Unload unused assets", () => Resources.UnloadUnusedAssets());
            DebugLogConsole.AddCommand("System_ClearCache", "Clear cache", () => Caching.ClearCache());
            DebugLogConsole.AddCommand("System_GarbageCollect", "Collect garbage", () => System.GC.Collect());

            DebugLogConsole.AddCommand<int>("System_LoadScene", "Load # scene (first index is 0)", index => SceneManager.LoadSceneAsync(index));
            DebugLogConsole.AddCommand<int>("System_Quit", "Quit application", index => Application.Quit());

            debugTools.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
                debugTools.SetActive(!debugTools.activeSelf);
        }
    }
}