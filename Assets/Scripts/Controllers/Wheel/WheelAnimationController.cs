using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using VertigoGames.Settings;
using VertigoGames.Utility;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelAnimationController
    {
        private readonly RectTransform _wheelTransform;
        private readonly RectTransform _indicatorTransform;
        private readonly RectTransform _wheelItemRoot;
        private readonly WheelSettings _settings;
        
        public WheelAnimationController(RectTransform wheelTransform, RectTransform indicatorTransform, RectTransform wheelItemRoot, WheelSettings settings)
        {
            _wheelTransform = wheelTransform;
            _indicatorTransform = indicatorTransform;
            _wheelItemRoot = wheelItemRoot;
            _settings = settings;
        }
       
        public void ResetWheel()
        {
            _wheelTransform.rotation = quaternion.identity;
            _wheelItemRoot.transform.DoBump(_settings.WheelScaleUpValue, _settings.WheelBumpDurationValue);
        }

        public void AnimateSpin(int rewardIndex, Action onComplete)
        {
            float targetAngle = GetTargetAngle(rewardIndex);
            float totalRotation = GetTotalRotation(targetAngle);
            int turnCount = (int)((totalRotation - 360) / GetRotatePerItem());

            AnimateIndicator(turnCount);
            RotateWheelAsync(totalRotation).OnComplete(() => onComplete?.Invoke());
        }
        
        private void AnimateIndicator(int turnCount)
        {
            _indicatorTransform
                .DORotate(new Vector3(0, 0, _settings.IndicatorRotationValue), _settings.IndicatorDurationValue)
                .SetLoops(turnCount - 1, LoopType.Yoyo)
                .SetEase(_settings.IndicatorEaseValue)
                .OnComplete(() =>
                {
                    _indicatorTransform.rotation = Quaternion.identity;
                });
        }

        private float GetTargetAngle(int rewardIndex)
        {
            return rewardIndex * GetRotatePerItem();
        }

        private float GetRotatePerItem()
        {
            return (360f / _settings.WheelSlotCountValue);
        }

        private float GetTotalRotation(float targetAngle)
        {
            return 360 * _settings.SpinRotationCountValue + targetAngle;
        }

        private Tween RotateWheelAsync(float totalRotation)
        {
            return _wheelTransform
                .DORotate(new Vector3(0, 0, -totalRotation), _settings.SpinDurationValue, RotateMode.FastBeyond360)
                .SetEase(_settings.SpinEaseValue)
                .SetRelative();
        }
    } 
}
