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
    }
    
    [System.Serializable]
    public class RewardInfo
    {
        public string Title;
        
        [PreviewField(Height = 100)] 
        public Sprite Icon;
        
        public RewardType RewardType;
    }
}

