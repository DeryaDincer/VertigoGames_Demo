using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VertigoGames.Datas.Reward;

namespace VertigoGames.Controllers.Reward
{
    public class RewardSelectController
    {
        private System.Random _random;

        private readonly double _desiredWeightProbability = 0.8; // Constant for 80% probability

        public List<RewardData> SelectRewards(ZoneData zoneData, int selectedRewardCount)
        {
            _random = new System.Random();
            
            var selectedRewards = new List<RewardData>();
            var availableRandomRewards = zoneData.RandomRewards.ToList(); // Copy of random rewards

            AddGuaranteedRewards(selectedRewards, zoneData.GuaranteedRewards, ref selectedRewardCount);
            AddRandomRewards(selectedRewards, availableRandomRewards, selectedRewardCount, zoneData.RewardWeight);

            return selectedRewards;
        }

        private void AddGuaranteedRewards(List<RewardData> selectedRewards, List<RewardData> guaranteedRewards, ref int selectedRewardCount)
        {
            selectedRewards.AddRange(guaranteedRewards);
            selectedRewardCount -= guaranteedRewards.Count;
        }

        private void AddRandomRewards(List<RewardData> selectedRewards, List<RewardData> availableRandomRewards, int count, RewardWeight rewardWeight)
        {
            for (int i = 0; i < count; i++)
            {
                var selectedReward = SelectRandomReward(availableRandomRewards, rewardWeight);
                if (selectedReward != null)
                {
                    availableRandomRewards.Remove(selectedReward); // Remove the selected reward from the list
                    selectedRewards.Add(selectedReward);
                }
                else
                {
                    Debug.LogWarning("Error: No more rewards available to select.");
                    break;
                }
            }
        }

        private RewardData SelectRandomReward(List<RewardData> rewards, RewardWeight rewardWeight)
        {
            double randomValue = _random.NextDouble();

            // 80% chance to select a reward with the desired weight
            if (randomValue < _desiredWeightProbability)
            {
                var desiredRewards = rewards.Where(r => r.RewardWeight == rewardWeight).ToList();
                var selectedReward = GetRandomRewardFromList(desiredRewards);
                if (selectedReward != null)
                {
                    return selectedReward;
                }
            }

            // 20% chance to select a reward from other weights
            var otherRewards = rewards.Where(r => r.RewardWeight != rewardWeight).ToList();
            return GetRandomRewardFromList(otherRewards);
        }

        private RewardData GetRandomRewardFromList(List<RewardData> rewardList)
        {
            if (rewardList.Any())
            {
                return rewardList[_random.Next(rewardList.Count)];
            }
            Debug.LogWarning("Error: No suitable rewards found in the list.");
            return null;
        }
    }
}