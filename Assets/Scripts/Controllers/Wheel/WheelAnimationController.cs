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
        private readonly RectTransform wheelTransform;
        private readonly RectTransform indicatorTransform;
        private readonly WheelSettings settings;
        
        public WheelAnimationController(RectTransform wheelTransform, RectTransform indicatorTransform, WheelSettings settings)
        {
            this.wheelTransform = wheelTransform;
            this.indicatorTransform = indicatorTransform;
            this.settings = settings;
        }
       
        public void ResetWheel()
        {
            wheelTransform.rotation = quaternion.identity;
            wheelTransform.transform.DoBump(settings.WheelScaleUpValue, settings.WheelBumpDurationValue);
        }

        public void AnimateSpin(int rewardIndex, Action onComplete)
        {
            float targetAngle = GetTargetAngle(rewardIndex);
            float totalRotation = GetTotalRotation(targetAngle);
            int turnCount = (int)totalRotation / 45;

            AnimateIndicator(turnCount, () => RotateWheelAsync(totalRotation).OnComplete(() => onComplete?.Invoke()));
        }
        
        private void AnimateIndicator(int turnCount, Action onComplete)
        {
            indicatorTransform
                .DORotate(new Vector3(0, 0, settings.IndicatorRotationValue), settings.IndicatorDurationValue)
                .SetLoops(turnCount - 1, LoopType.Yoyo)
                .SetEase(settings.IndicatorEaseValue)
                .OnComplete(() =>
                {
                    indicatorTransform.rotation = Quaternion.identity;
                    onComplete?.Invoke();
                });
        }

        private float GetTargetAngle(int rewardIndex)
        {
            return rewardIndex * (360f / settings.WheelSlotCountValue);
        }

        private float GetTotalRotation(float targetAngle)
        {
            return 360 * settings.SpinRotationCountValue + targetAngle;
        }

        private Tween RotateWheelAsync(float totalRotation)
        {
            return wheelTransform
                .DORotate(new Vector3(0, 0, -totalRotation), settings.SpinDurationValue, RotateMode.FastBeyond360)
                .SetEase(settings.SpinEaseValue)
                .SetRelative();
        }
    } 
}
