using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;

public class UIAnimationItem : MonoBehaviour
{
    public RewardData RewardData => _rewardData;
    public int RewardAmount => _rewardAmount;

    [SerializeField] protected RectTransform _itemRoot;
    [SerializeField] protected Image _itemImageValue;

    private RewardData _rewardData;
    private int _rewardAmount;

    public void SetItem(RewardData rewardData)
    {
        SetRewardData(rewardData);
        SetItemSprite();
    }
    
    public virtual void SetRewardData(RewardData rewardData)
    {
        _rewardData = rewardData;
    }

    public virtual void SetItemSprite()
    {
        _itemImageValue.sprite = _rewardData.RewardInfo.Icon;
    }
    
}
