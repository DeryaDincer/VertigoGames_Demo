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

        [SerializeField] protected Image itemImageValue;
        [SerializeField] protected TextMeshProUGUI rewardAmountTextValue;

        private RewardData _rewardData;
        private int _rewardAmount;

        public override void OnDeactivate() { }
        public override void OnSpawn() { }
        public override void OnCreated() { }
        public virtual void SetRewardData(RewardData rewardData) => _rewardData = rewardData;
       
        public virtual void SetItemSprite() =>  itemImageValue.sprite = _rewardData.RewardInfo.Icon;

        public virtual void SetRewardAmount(int rewardAmount)
        {
            _rewardAmount = rewardAmount;
            rewardAmountTextValue.text = "x" + _rewardAmount;
        }
    }
}