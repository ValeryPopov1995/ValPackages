using UnityEngine;

namespace ValPackage.Common
{
    public class ApplicationMethods : MonoBehaviour
    {
        public void Quit() => Application.Quit();

        public void OpenURL(string url) => Application.OpenURL(url);
    }
}