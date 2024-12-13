using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace ValeryPopov.Common.Extantions
{
    public static class DoTweenExtantions
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