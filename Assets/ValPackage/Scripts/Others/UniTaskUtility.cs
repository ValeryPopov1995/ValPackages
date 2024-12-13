using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ValeryPopov.Common
{
    public struct UniTaskUtility
    {
        internal static async Task AwaitUnityEvent(UnityEvent unityEvent)
        {
            bool togle = false;
            unityEvent.AddListener(SetTrue);
            await Cysharp.Threading.Tasks.UniTask.WaitWhile(() => !togle);
            unityEvent.RemoveListener(SetTrue);

            void SetTrue() => togle = true;
        }

        internal static async Task AwaitAction(Action action)
        {
            bool togle = false;
            action += SetTrue;
            await Cysharp.Threading.Tasks.UniTask.WaitWhile(() => !togle);
            action -= SetTrue;

            void SetTrue() => togle = true;
        }

        internal static async Task DelayWithPause(float duration, Func<bool> isPaused)
        {
            while (duration > 0)
            {
                if (!isPaused())
                    duration -= Time.deltaTime;

                await Task.Yield();
            }
        }
    }
}