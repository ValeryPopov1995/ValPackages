#if SERVICES
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Services
{
    public class UnityAuthenticationInitialization : MonoBehaviour, IInitializable
    {
        [SerializeField] private UnityServicesInitialization _unityServices;

        public bool Initialized { get; private set; }

        public async Task Initialize()
        {
            // init it after UnityServices initialization

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Initialized = true;
                this.Log("initialized");
            }
            else
                Initialized = false;
        }
    }
}
#endif