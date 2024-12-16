using UnityEngine;
using Zenject;

namespace ValPackage.Common.PauseSystem
{
    public abstract class PausableMonoBehaviour : MonoBehaviour
    {
        [Inject] protected Pauser _pauser;
        [SerializeField] private bool _pausable = true;

        protected virtual void Start()
        {
            _pauser.OnPaused += PauseInternal;
            _pauser.OnResumed += ResumeInternal;
        }

        protected virtual void OnDestroy()
        {
            _pauser.OnPaused -= PauseInternal;
            _pauser.OnResumed -= ResumeInternal;
        }

        private void PauseInternal()
        {
            if (_pausable) Pause();
        }
        private void ResumeInternal()
        {
            if (_pausable) Resume();
        }

        protected abstract void Pause();
        protected abstract void PauseImmidiatly();
        protected abstract void Resume();
    }
}