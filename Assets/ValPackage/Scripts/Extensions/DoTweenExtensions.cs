using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace ValPackage.Common.Extensions
{
    public static class DoTweenExtensions
    {
        public static TweenerCore<Color, Color, ColorOptions> DoAlpha(this Image image, float endValue, float duration)
        {
            var c = image.color;
            c.a = endValue;
            return image.DOColor(c, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> DoAlpha(this RawImage rawImage, float endValue, float duration)
        {
            var c = rawImage.color;
            c.a = endValue;
            return rawImage.DOColor(c, duration);
        }
    }
}