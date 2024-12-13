using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ValeryPopov.Common.Extantions;

namespace ValeryPopov.Common.PauseSystem
{
    /// <summary>
    /// Устанавливает все найденные IPausable на паузу
    /// </summary>
    public class Pauser
    {
        public Action OnPaused, OnResumed;
        public bool IsPaused { get; private set; }

        public bool FindIPausable = false;



        [Button]
        public void Pause()
        {
            IsPaused = true;

            if (FindIPausable)
                UpdateIPausables(pausable => pausable.Pause(), "paused");

            this.Log($"paused");
            OnPaused?.Invoke();
        }

        [Button]
        public void Resume()
        {
            IsPaused = false;

            if (FindIPausable)
                UpdateIPausables(pausable => pausable.Resume(), "resumed");

            this.Log($"resumed");
            OnResumed?.Invoke();
        }

        [Button]
        public void Change()
        {
            if (IsPaused) Resume();
            else Pause();
        }



        private void UpdateIPausables(Action<IPausable> action, string logAction)
        {
            var pausables = GetIPausables();

            foreach (var pausable in pausables)
                action.Invoke(pausable);

            this.Log($"{logAction} {pausables.Count()} IPausable");
        }

        private IEnumerable<IPausable> GetIPausables()
        {
            return UnityEngine.Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                .Where(x => x is IPausable)
                .Select(x => x as IPausable);
        }
    }
}