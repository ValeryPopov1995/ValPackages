using UnityEngine;

namespace ValPackage.Common.App
{
    public class ApplicationStartup : MonoBehaviour
    {
        [SerializeField] private ThreadPriority _backgroundLoadingPriority = ThreadPriority.Low;

        private void Awake()
        {
            Application.backgroundLoadingPriority = _backgroundLoadingPriority;
        }
    }
}