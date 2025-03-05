using DG.Tweening;
using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;
using VertigoGames.Utility;

namespace VertigoGames.UI.Item.Reward
{
    public class RewardAreaItem : BaseItem
    {
        public void SetItem(RewardData rewardData)
        {
            SetRewardData(rewardData);
            SetItemSprite();
        }
        
        public virtual void SetRewardAmountWithAnimation(int startRewardAmount, int addRewardAmount)
        {
            DOTween.Kill(rewardAmountTextValue.transform);
            rewardAmountTextValue.DoCount(startRewardAmount, (startRewardAmount + addRewardAmount));
        }
    }
}

