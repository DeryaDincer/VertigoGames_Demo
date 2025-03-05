using System;
using DG.Tweening;
using UnityEngine;
using VertigoGames.Settings;

namespace VertigoGames.Controllers.Zone
{
    public class ZoneBarAnimationController
    {
        private readonly ZoneBarSettings _zoneBarSettings;
        private readonly RectTransform _layoutGroupRect;
        private readonly float _slideDuration;
        private readonly Ease _slideEase;
        private readonly float _slideDistance;
        private int _slideCount;

        public ZoneBarAnimationController(RectTransform layoutGroupRect,ZoneBarSettings zoneBarSettings)
        {
            _zoneBarSettings = zoneBarSettings;
            _layoutGroupRect = layoutGroupRect;
            _slideDuration = zoneBarSettings.SlideDuration;
            _slideEase = zoneBarSettings.SlideEase;
            _slideDistance = zoneBarSettings.SlideDistance;
        }

        public void SlideToNextZone(Action<bool> onSlideComplete)
        {
            float currentPositionX = _layoutGroupRect.anchoredPosition.x;
            Sequence slideSequence = DOTween.Sequence();

            slideSequence.Append(_layoutGroupRect.DOAnchorPosX(currentPositionX - _slideDistance, _slideDuration)
                .SetEase(_slideEase));

            slideSequence.AppendCallback(() => SetSlideCompleteAction(onSlideComplete));
            slideSequence.AppendCallback(() => IncreaseSlideCount());
        }

        private void SetSlideCompleteAction(Action<bool> onSlideComplete)
        {
            bool shouldResetPosition = false;
            if (_slideCount >= _zoneBarSettings.AverageIndicatorIndex)
            {
                shouldResetPosition = true;
                ResetPosition();
            }

            onSlideComplete?.Invoke(shouldResetPosition);
        }

        private void IncreaseSlideCount()
        {
            _slideCount++;
        }
        
        public void ResetPosition()
        {
            _layoutGroupRect.anchoredPosition = Vector2.zero;
        }
        
        public void ResetBarData()
        {
            _slideCount = 0;
        }
    }
}