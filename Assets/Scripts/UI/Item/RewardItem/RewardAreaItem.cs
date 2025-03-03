using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;

public class RewardAreaItem : BaseItem
{
    public void SetItem(RewardData rewardData, int rewardAmount)
    {
        SetRewardData(rewardData);
        SetItemSprite();
        SetRewardAmount(rewardAmount);
    }

}
