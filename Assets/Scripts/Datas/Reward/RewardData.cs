using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Datas.Reward
{
    [CreateAssetMenu (fileName = "Reward", menuName = "VertigoGames/Reward")]
    public class RewardData : ScriptableObject
    {
        [Title("Reward Info")]
        [SerializeField] private RewardInfo rewardInfo;

        [Title("Weight Settings")]
        [SerializeField] private RewardWeight _rewardWeight;
       
        public RewardInfo RewardInfo => rewardInfo;
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
        
        [Title("Visuals")]
        [PreviewField(Height = 100)] 
        public Sprite Icon;
    }
}

