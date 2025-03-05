using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "UIAnimationSettings", menuName = "VertigoGames/UIAnimationSettings")]
    public class UIAnimationSettings : ScriptableObject
    {
        [Title("UI Animation Manager Animation Settings")] 
        
        [SerializeField] private float _spawnRadius = 50f;
        [SerializeField] private float _spawnDelayMultiplier = 0.1f;
        [SerializeField] private int _maxRewardCount = 5;
        
        [Title("UI Animation Item Animation Settings")] 
        
        [SerializeField] private float _pathDurationBase = 0.6f;
        [SerializeField] private float _middlePositionYOffset = 50f;
        [SerializeField] private float _targetScale = 0.6f;
       
        #region Public References
        public float SpawnRadius => _spawnRadius;
        public float SpawnDelayMultiplier => _spawnDelayMultiplier;
        public int MaxRewardCount => _maxRewardCount;
        public float PathDurationBase => _pathDurationBase;
        public float MiddlePositionYOffset => _middlePositionYOffset;
        public float TargetScale => _targetScale;
        #endregion
    }
}