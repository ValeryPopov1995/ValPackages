#if SERVICES
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.Services
{
    public class UnityServicesInitialization : MonoBehaviour, IInitializable
    {
        public enum EnvironmentType
        {
            production,
            release
        }

        [SerializeField] private EnvironmentType _environment = EnvironmentType.production;

        public bool Initialized { get; private set; }

        public async Task Initialize()
        {
            try
            {
                var options = new InitializationOptions();
                options.SetEnvironmentName(_environment.ToString());
                await UnityServices.InitializeAsync(options);
                Initialized = true;
                this.Log("UnityServices InitializeAsync completed");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
#endif