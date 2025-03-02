using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Datas.Reward
{
    [CreateAssetMenu (fileName = "Reward", menuName = "VertigoGames/Reward")]
    public class RewardData : ScriptableObject
    {
        public RewardInfo RewardInfo => rewardInfo;
        [SerializeField] private RewardInfo rewardInfo;
        
        [Title("Weight Settings")]
        [SerializeField] private RewardWeight _rewardWeight;
        public RewardWeight RewardWeight => _rewardWeight;
    }
    
    [System.Serializable]
    public class RewardInfo
    {
        [Title("General Info")]
        public string Title;
        public RewardType RewardType;
    
        [Title("Reward Calculation")] 
        public int InitialRewardCount;
        public float RewardIncrementMultiplier;
        
        [Title("Visuals")]
        [PreviewField(Height = 100)] 
        public Sprite Icon;
    }
}

