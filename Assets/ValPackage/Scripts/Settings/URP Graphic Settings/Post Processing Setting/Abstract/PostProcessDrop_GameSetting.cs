using UnityEngine.Rendering;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public abstract class PostProcessDrop_GameSetting<T> : Int_GameSetting where T : VolumeComponent
    {
        [Inject] private Volume _volume;

        public override void Apply()
        {
            base.Apply();
            if (_volume.profile.TryGet(out T volumeComponent))
                ApplyValue(volumeComponent);
        }

        protected abstract void ApplyValue(T volumeComponent);
    }
}