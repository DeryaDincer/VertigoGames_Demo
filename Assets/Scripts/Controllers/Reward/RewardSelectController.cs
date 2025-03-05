using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Datas.Reward;

namespace VertigoGames.Controllers.Reward
{
    public class RewardSelectController
    {
        private readonly System.Random _random = new System.Random();
        private double _desiredWeightProbability = 0.8; // %80 probability

        public List<RewardData> SelectRewards(ZoneData zoneData, int totalRewardCount)
        {
            if (zoneData == null)
            {
                Debug.LogError("ZoneData is null! Cannot select rewards.");
                return new List<RewardData>();
            }

            var selectedRewards = new List<RewardData>();
            var availableRewards = zoneData.RandomRewards.ToList();

            AddGuaranteedRewards(selectedRewards, zoneData.GuaranteedRewards, ref totalRewardCount);
            AddRandomRewards(selectedRewards, availableRewards, totalRewardCount, zoneData.RewardWeight);

            return selectedRewards.OrderBy(_ => _random.Next()).ToList();
        }

        private void AddGuaranteedRewards(List<RewardData> selectedRewards, List<RewardData> guaranteedRewards, ref int remainingRewardCount)
        {
            if (guaranteedRewards == null || guaranteedRewards.Count == 0)
                return;

            selectedRewards.AddRange(guaranteedRewards);
            remainingRewardCount -= guaranteedRewards.Count;
        }

        private void AddRandomRewards(List<RewardData> selectedRewards, List<RewardData> availableRewards, int count, RewardWeight targetWeight)
        {
            for (int i = 0; i < count; i++)
            {
                var selectedReward = SelectRandomReward(availableRewards, targetWeight);
                if (selectedReward != null)
                {
                    availableRewards.Remove(selectedReward);
                    selectedRewards.Add(selectedReward);
                }
                else
                {
                    Debug.LogWarning("No more available rewards to select.");
                    break;
                }
            }
        }

        private RewardData SelectRandomReward(List<RewardData> rewards, RewardWeight targetWeight)
        {
            if (rewards == null || rewards.Count == 0)
            {
                Debug.LogWarning("Reward list is empty, cannot select a random reward.");
                return null;
            }

            double randomValue = _random.NextDouble();

            if (randomValue < _desiredWeightProbability)
            {
                var filteredRewards = rewards.Where(r => r.RewardWeight == targetWeight).ToList();
                var selectedReward = GetRandomReward(filteredRewards);
                if (selectedReward != null)
                    return selectedReward;
            }

            var otherRewards = rewards.Where(r => r.RewardWeight != targetWeight).ToList();
            return GetRandomReward(otherRewards);
        }

        private RewardData GetRandomReward(List<RewardData> rewardList)
        {
            if (rewardList == null || rewardList.Count == 0)
            {
                Debug.LogWarning("No suitable rewards found in the list.");
                return null;
            }

            return rewardList[_random.Next(rewardList.Count)];
        }
    }
}
