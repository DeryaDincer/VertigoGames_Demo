using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using VertigoGames.Settings;
using Random = UnityEngine.Random;

namespace VertigoGames.Controllers.Wheel
{
    public class WheelAnimationController
    {
        private RectTransform wheelContainer;
        private int itemCount;
        private int spinRotationCount;
        private float spinDuration;
        private Ease ease;
        
        public WheelAnimationController(RectTransform wheelContainer, WheelSettings wheelSettings, int itemCount)
        {
            this.wheelContainer = wheelContainer;
            this.itemCount = itemCount;
            spinRotationCount = wheelSettings.SpinRotationCountValue;
            spinDuration = wheelSettings.SpinDurationValue;
            ease = wheelSettings.SpinEaseValue;
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
            return Random.Range(0, itemCount);
        }

        private float CalculateTargetAngle(int index)
        {
            return index * (360f / itemCount);
        }

        private float CalculateTotalRotation(float targetAngle)
        {
            return 360 * spinRotationCount + targetAngle;
        }

        private Tween RotateWheel(float totalRotation)
        {
            Tween tween = wheelContainer.DORotate(new Vector3(0, 0, -totalRotation), spinDuration, RotateMode.FastBeyond360)
                .SetEase(ease)
                .SetRelative();

            return tween;
        }
    } 
}