using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VertigoGames.Controllers.Reward;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "GamePrefabSettings", menuName = "VertigoGames/GamePrefabSettings")]
    public class GamePrefabSettings : ScriptableObject
    {
        [Title("Data References")] 
        [SerializeField] private List<ZoneData> _zoneDatas;
        
        [Title("Controller Prefabs")]
        
        [SerializeField] private WheelController _wheelController;
        [SerializeField] private ZoneBarController _zoneBarController;
        [SerializeField] private RewardAreaController _rewardAreaController;
        
        #region Public References
        public List<ZoneData> ZoneDatas => _zoneDatas;
        public WheelController WheelController => _wheelController;
        public ZoneBarController ZoneBarController => _zoneBarController;
        public RewardAreaController RewardAreaController => _rewardAreaController;
        #endregion
    }
}