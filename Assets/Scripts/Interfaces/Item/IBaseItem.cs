using VertigoGames.Datas.Reward;

namespace VertigoGames.Interfaces.Item
{
    public interface IBaseItem
    { 
        void SetRewardData(RewardData rewardData);
        void SetItemSprite();
        void SetRewardAmount(int rewardAmount);
    }
}