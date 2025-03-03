using DG.Tweening;
using UnityEngine;
using VertigoGames.Settings;

namespace VertigoGames.Controllers.Zone
{
    public class ZoneBarAnimationController
    {
        private readonly RectTransform _layoutGroupRect;
        private readonly float _slideDuration;
        private readonly Ease _slideEase;
        private readonly float _slideDistance;

        public ZoneBarAnimationController(RectTransform layoutGroupRect,ZoneBarSettings zoneBarSettings)
        {
            _layoutGroupRect = layoutGroupRect;
            _slideDuration = zoneBarSettings.SlideDuration;
            _slideEase = zoneBarSettings.SlideEase;
            _slideDistance = zoneBarSettings.SlideDistance;
        }

        public void SlideToNextZone(System.Action onSlideComplete)
        {
            float currentPositionX = _layoutGroupRect.anchoredPosition.x;
            Sequence slideSequence = DOTween.Sequence();

            slideSequence.Append(_layoutGroupRect.DOAnchorPosX(currentPositionX - _slideDistance, _slideDuration)
                .SetEase(_slideEase));

            slideSequence.AppendCallback(() => onSlideComplete?.Invoke());
        }

        public void ResetPosition()
        {
            _layoutGroupRect.anchoredPosition = Vector2.zero;
        }
    }
}