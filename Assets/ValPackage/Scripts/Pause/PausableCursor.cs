using UnityEngine;

namespace ValeryPopov.Common.PauseSystem
{
    public class PausableCursor : PausableMonoBehaviour
    {
        [SerializeField] private bool _lockOnAwake;
        private void Awake()
        {
            if (_lockOnAwake)
                Lock();
        }

        protected override void Pause()
        {
            Unlock();
        }

        protected override void PauseImmidiatly()
        {
            Unlock();
        }

        protected override void Resume()
        {
            Lock();
        }

        private void Lock()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Unlock()
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}