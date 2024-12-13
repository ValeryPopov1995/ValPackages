using UnityEngine.Rendering;
using Zenject;

namespace ValeryPopov.Common.Settings.GraphicSettings
{
    public abstract class PostProcessActive_GameSetting<T> : Bool_GameSetting where T : VolumeComponent
    {
        [Inject] protected Volume _volume;

        public override void Apply()
        {
            base.Apply();
            if (_volume.profile.TryGet(out T volumeComponent))
                volumeComponent.active = _value;
        }
    }
}