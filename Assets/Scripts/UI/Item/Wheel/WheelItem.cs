using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;

namespace VertigoGames.UI.Item.Wheel
{
    public class WheelItem : BaseItem
    {
        public void SetItem(RewardData rewardData, int rewardAmount, int itemIndex, float wheelRadius, int wheelSlotCount)
        {
            SetRewardData(rewardData);
            SetItemSprite();
            SetRewardAmount(rewardAmount);
            PositionAndRotateItemOnWheel(itemIndex, wheelRadius,wheelSlotCount);
        }

        private void PositionAndRotateItemOnWheel(int itemIndex, float wheelRadius, int wheelSlotCount)
        {
            Vector2 itemPosition = CalculateItemPosition(itemIndex, wheelRadius, wheelSlotCount);
            transform.localPosition = itemPosition;

            float itemRotation = CalculateItemRotation(itemPosition);
            transform.localRotation = Quaternion.Euler(0, 0, itemRotation - 90);
        }

        private Vector2 CalculateItemPosition(int itemIndex, float wheelRadius, int wheelSlotCount, float startAngleOffset = 90f)
        {
            float itemAngleOffset = (360f / wheelSlotCount);
            float itemAngle = itemIndex * itemAngleOffset + startAngleOffset;
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