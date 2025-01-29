using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using ValPackage.Common.Extensions;

namespace ValPackage.Common
{
    [Serializable]
    public class AsyncCircle : IDisposable
    {
        public bool Canceled =>
            _canceledInside
            || _tokens.Any(token => token.IsCancellationRequested)
            || _sources.Any(source => source.IsCancellationRequested);
        private bool _canceledInside;
        // TODO pause resume

        /// <summary>
        /// Delay between action invoke, assighned in Inspector, Constructor or Initialuze method.
        /// </summary>
        [field: SerializeField] public float UpdateDelay { get; private set; } = .1f;

        public bool Initialized { get; private set; }

        public CustomEvent OnStart, OnEnd, OnAsyncUpdate;

        private Action _updateAction;
        private CancellationToken[] _tokens;
        private CancellationTokenSource[] _sources;



        public AsyncCircle(Action updateAction, CancellationToken[] tokens = null, CancellationTokenSource[] sources = null, float updateDelay = .1f)
        {
            Initialize(updateAction, tokens, sources, updateDelay);
        }



        /// <summary>
        /// Use it if <see cref="Initialized"/>
        /// </summary>
        public void Start()
        {
            if (!Initialized) return;
            Update();
            OnStart.TryInvoke();
        }

        public void Initialize(Action updateAction, CancellationToken[] tokens = null, CancellationTokenSource[] sources = null, float updateDelay = default)
        {
            if (Initialized) return; // TODO can be last Update before second Initialize()

            _updateAction = updateAction;

            if (tokens == null)
                _tokens = new CancellationToken[0];
            else
                _tokens = tokens;

            if (sources == null)
                _sources = new CancellationTokenSource[0];
            else
                _sources = sources;

            if (updateDelay != default)
                UpdateDelay = updateDelay;

            Initialized = true;
        }

        public void Cancel() => _canceledInside = true;

        private async void Update()
        {
            _canceledInside = false;

            if (UpdateDelay <= 0)
            {
                UpdateDelay = .1f;
                this.LogWarning("update delay <= 0, changed to default = " + UpdateDelay);
            }

            while (!Canceled)
            {
                _updateAction();
                OnAsyncUpdate.TryInvoke();
                await Task.Delay(TimeSpan.FromSeconds(UpdateDelay));
            }

            OnEnd.TryInvoke();
        }

        public void Dispose()
        {
            Cancel();

            _updateAction = null;
            UpdateDelay = default;
            _sources = null;
            _tokens = null;

            Initialized = false;
        }
    }
}