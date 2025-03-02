using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;

namespace VertigoGames.UI.Item.Wheel
{
    public class WheelItem : BaseItem
    {
        public void SetItem(RewardData rewardData, int rewardAmount, int itemIndex, float wheelRadius)
        {
            SetRewardData(rewardData);
            SetItemSprite();
            SetRewardAmount(rewardAmount);
        }
    }
}