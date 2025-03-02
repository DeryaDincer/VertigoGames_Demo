using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VertigoGames.Datas.Reward;

namespace VertigoGames.Controllers.Reward
{
    public class RewardSelectController : MonoBehaviour
    {
        private List<RewardData> rewardDatas;
        public RewardSelectController(List<RewardData> rewardDatas)
        {
            this.rewardDatas = rewardDatas;
        }

        public List<RewardData> GetRewardDatas()
        {
            return rewardDatas;
        }
    }
}

