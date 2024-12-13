#if SERVICES
using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Services
{
    /// <summary>
    /// Load Remote Config from Unity Cloud. <see href="https://docs.unity3d.com/Packages/com.unity.remote-config@3.3/manual/CodeIntegration.html">Manual</see>
    /// </summary>
    /// 
    /// <typeparam name="TUserAttributes">Custom struct with user params
    /// <example><code>
    /// public struct userAttributes
    /// {
    ///     // Optionally declare variables for any custom user attributes:
    ///     public bool expansionFlag;
    /// }
    /// </code></example></typeparam>
    /// 
    /// <typeparam name="TAppAttributes">Custom struct with app params
    /// <example><code>
    /// public struct appAttributes
    /// {
    ///     // Optionally declare variables for any custom app attributes:
    ///     public int level;
    ///     public int score;
    ///     public string appVersion;
    /// }
    /// </code></example></typeparam>
    ///
    /// <typeparam name="TFilterAttributes">Custom struct with filter params
    /// <example><code>
    /// public struct filterAttributes
    /// {
    ///    // Optionally declare variables for attributes to filter on any of following parameters:
    ///    public string[] key;
    ///    public string[] type;
    ///    public string[] schemaId;
    /// }
    /// </code></example></typeparam>
    public class UnityRemoteConfigInitialization<TUserAttributes, TAppAttributes, TFilterAttributes> : MonoBehaviour, IInitializable
        where TUserAttributes : struct
        where TAppAttributes : struct
        where TFilterAttributes : struct
    {

        public bool Initialized { get; private set; }

        [SerializeField] private UnityAuthenticationInitialization _unityAuthentication;
        public static RuntimeConfig Config => RemoteConfigService.Instance.appConfig;



        protected virtual void Awake()
        {
            RemoteConfigService.Instance.FetchCompleted += LogFetch;
        }

        public async Task Initialize()
        {
            // init it after UnityServices initialization

            if (_unityAuthentication.Initialized)
            {
                await RemoteConfigService.Instance.FetchConfigsAsync(new TUserAttributes(), new TAppAttributes());
                Initialized = true;
                this.Log("initialized");
            }
            else
                Initialized = false;
        }

        private void LogFetch(ConfigResponse configResponse)
        {
            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    this.Log("No settings loaded this session and no local cache file exists; using default values.");
                    break;
                case ConfigOrigin.Cached:
                    this.Log("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    this.Log("New settings loaded this session; update values accordingly.");
                    break;
            }
        }
    }
}
#endif