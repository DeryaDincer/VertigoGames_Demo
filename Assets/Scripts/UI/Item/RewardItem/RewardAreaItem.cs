using VertigoGames.Datas.Reward;
using VertigoGames.UI.Item.Base;

namespace VertigoGames.UI.Item.Reward
{
    public class RewardAreaItem : BaseItem
    {
        public void SetItem(RewardData rewardData)
        {
            SetRewardData(rewardData);
            SetItemSprite();
        }
        
        public virtual void SetRewardAmount(int startRewardAmount, int endRewardAmount)
        {
            
           // int rewardAmount
          //  _rewardAmountTextValue.text = "x" + rewardAmount;
        }
    }
}

