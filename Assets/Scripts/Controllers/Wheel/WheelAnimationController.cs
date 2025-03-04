using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using VertigoGames.Settings;
using VertigoGames.Utility;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelAnimationController
    {
        private RectTransform _wheelContainer;
        private RectTransform _wheelIndicator;
        private WheelSettings _wheelSettings;
        
        public WheelAnimationController(RectTransform wheelContainer, RectTransform wheelIndicator, WheelSettings wheelSettings)
        {
            _wheelContainer = wheelContainer;
            _wheelIndicator = wheelIndicator;
            _wheelSettings = wheelSettings;
        }
       
        public void ResetWheelAnimation()
        {
            _wheelContainer.rotation = quaternion.identity;
            _wheelContainer.transform.DoBump(_wheelSettings.WheelScaleUpValue, _wheelSettings.WheelBumpDurationValue);
        }
     
        
        public void SpinWheel(int targetRewardIndex, Action completeAction)
        {
            float targetAngle = CalculateTargetAngle(targetRewardIndex);
            float totalRotation = CalculateTotalRotation(targetAngle);
            int turnAmount = (int)totalRotation / 45;

            StartIndicatorAnimation(turnAmount);
            RotateWheel(totalRotation).OnComplete(() =>
            {
                completeAction.Invoke();
            });
        }
        
        private void StartIndicatorAnimation(int turnAmount)
        {
            _wheelIndicator.DORotate(new Vector3(0, 0, _wheelSettings.IndicatorRotationValue),
                    _wheelSettings.IndicatorDurationValue)
                .SetLoops(turnAmount - 1, LoopType.Yoyo)
                .SetEase(_wheelSettings.IndicatorEaseValue)
                .OnComplete(() => _wheelIndicator.rotation = Quaternion.identity);
        }
        
        private float CalculateTargetAngle(int index)
        {
            return index * (360f / _wheelSettings.WheelSlotCountValue);
        }

        private float CalculateTotalRotation(float targetAngle)
        {
            return 360 * _wheelSettings.SpinRotationCountValue + targetAngle;
        }

        private Tween RotateWheel(float totalRotation)
        {
            Tween tween = _wheelContainer.DORotate(new Vector3(0, 0, -totalRotation), _wheelSettings.SpinDurationValue, RotateMode.FastBeyond360)
                .SetEase(_wheelSettings.SpinEaseValue)
                .SetRelative();

            return tween;
        }
    } 
}