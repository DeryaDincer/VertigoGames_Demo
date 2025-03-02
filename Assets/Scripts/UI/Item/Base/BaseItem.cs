using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Interfaces.Item;

namespace VertigoGames.UI.Item.Base
{
    public class BaseItem : MonoBehaviour, IBaseItem
    {
        public RewardData RewardData => _rewardData;

        [SerializeField] protected Image _itemImageValue;
        [SerializeField] protected TextMeshProUGUI _rewardAmountTextValue;

        private RewardData _rewardData;

        public virtual void SetRewardData(RewardData rewardData)
        {
            _rewardData = rewardData;
        }

        public virtual void SetItemSprite()
        {
            _itemImageValue.sprite = _rewardData.RewardInfo.Icon;
        }

        public virtual void SetRewardAmount(int rewardAmount)
        {
            _rewardAmountTextValue.text = "x" + rewardAmount;
        }
    }
}