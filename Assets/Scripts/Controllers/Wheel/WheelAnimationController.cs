using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using VertigoGames.Settings;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelAnimationController
    {
        private RectTransform wheelContainer;
        private int _wheelSlotCountValue;
        private int _spinRotationCount;
        private float _spinDuration;
        private Ease _ease;
        
        public WheelAnimationController(RectTransform wheelContainer, WheelSettings wheelSettings)
        {
            this.wheelContainer = wheelContainer;
            _wheelSlotCountValue = wheelSettings.WheelSlotCountValue;
            _spinRotationCount = wheelSettings.SpinRotationCountValue;
            _spinDuration = wheelSettings.SpinDurationValue;
            _ease = wheelSettings.SpinEaseValue;
        }
       
        public void ResetWheelAnimation()
        {
            wheelContainer.rotation = quaternion.identity;
        }
        
        public void SpinWheel(Action completeAction)
        {
            int randomIndex = GetRandomItemIndex();
            float targetAngle = CalculateTargetAngle(randomIndex);
            float totalRotation = CalculateTotalRotation(targetAngle);

            RotateWheel(totalRotation).OnComplete(() => completeAction.Invoke());
        }

        private int GetRandomItemIndex()
        {
            return Random.Range(0, _wheelSlotCountValue);
        }

        private float CalculateTargetAngle(int index)
        {
            return index * (360f / _wheelSlotCountValue);
        }

        private float CalculateTotalRotation(float targetAngle)
        {
            return 360 * _spinRotationCount + targetAngle;
        }

        private Tween RotateWheel(float totalRotation)
        {
            Tween tween = wheelContainer.DORotate(new Vector3(0, 0, -totalRotation), _spinDuration, RotateMode.FastBeyond360)
                .SetEase(_ease)
                .SetRelative();

            return tween;
        }
    } 
}