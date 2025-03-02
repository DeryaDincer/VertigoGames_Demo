using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;

namespace VertigoGames.UI.Item.Wheel
{
    public class WheelItem : BaseItem
    {
        private readonly float _itemAngleOffset = 45f;
        
        public void SetItem(RewardData rewardData, int rewardAmount, int itemIndex, float wheelRadius)
        {
            SetRewardData(rewardData);
            SetItemSprite();
            SetRewardAmount(rewardAmount);
            PositionAndRotateItemOnWheel(itemIndex, wheelRadius);
        }

        private void PositionAndRotateItemOnWheel(int itemIndex, float wheelRadius)
        {
            Vector2 itemPosition = CalculateItemPosition(itemIndex, wheelRadius);
            transform.localPosition = itemPosition;

            float itemRotation = CalculateItemRotation(itemPosition);
            transform.localRotation = Quaternion.Euler(0, 0, itemRotation - 90);
        }

        private Vector2 CalculateItemPosition(int itemIndex, float wheelRadius)
        {
            float itemAngle = itemIndex * _itemAngleOffset; 
            float itemRadian = itemAngle * Mathf.Deg2Rad;

            float itemX = Mathf.Cos(itemRadian) * wheelRadius;
            float itemY = Mathf.Sin(itemRadian) * wheelRadius;

            return new Vector2(itemX, itemY);
        }

        private float CalculateItemRotation(Vector2 itemPosition)
        {
            return Mathf.Atan2(itemPosition.y, itemPosition.x) * Mathf.Rad2Deg;
        }
    }
}