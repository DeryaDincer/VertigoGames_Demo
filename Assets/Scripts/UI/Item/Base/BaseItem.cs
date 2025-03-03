using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VertigoGames.Datas.Reward;
using VertigoGames.Interfaces.Item;
using VertigoGames.Pooling;

namespace VertigoGames.UI.Item.Base
{
    public class BaseItem : PoolObject, IBaseItem
    {
        public RewardData RewardData => _rewardData;
        public int RewardAmount => _rewardAmount;

        [SerializeField] protected RectTransform _itemRoot;
        [SerializeField] protected Image _itemImageValue;
        [SerializeField] protected TextMeshProUGUI _rewardAmountTextValue;

        private RewardData _rewardData;
        private int _rewardAmount;

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
            _rewardAmount = rewardAmount;
            _rewardAmountTextValue.text = "x" + _rewardAmount;
        }

        public override void OnDeactivate()
        {
            
        }

        public override void OnSpawn()
        {
            transform.localScale = Vector3.one;
        }

        public override void OnCreated()
        {
           
        }
    }
}