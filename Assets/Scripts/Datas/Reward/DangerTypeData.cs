using System.Collections.Generic;
using UnityEngine;

namespace VertigoGames.Datas.Reward
{
    [CreateAssetMenu (fileName = "DangerTypeData", menuName = "VertigoGames/DangerTypeData")]
    public class DangerTypeData : ScriptableObject
    {
        [SerializeField] private List<RewardType> dangerTypes;

        public bool IsDangerType(RewardType rewardType)
        {
            return dangerTypes.Contains(rewardType);
        }
    }
}