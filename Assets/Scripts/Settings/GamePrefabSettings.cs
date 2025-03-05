using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using VertigoGames.Controllers.Currency;
using VertigoGames.Controllers.Reward;
using VertigoGames.Controllers.Wheel;
using VertigoGames.Controllers.Zone;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "GamePrefabSettings", menuName = "VertigoGames/GamePrefabSettings")]
    public class GamePrefabSettings : ScriptableObject
    {
        [Title("Controller Prefabs")]
        
        [SerializeField] private WheelController wheelController;
        [SerializeField] private ZoneBarController zoneBarController;
        [SerializeField] private RewardAreaController rewardAreaController;
        [SerializeField] private CurrencyController currencyController;
        
        #region Public References
        public WheelController WheelController => wheelController;
        public ZoneBarController ZoneBarController => zoneBarController;
        public RewardAreaController RewardAreaController => rewardAreaController;
        public CurrencyController CurrencyController => currencyController;
        #endregion
    }
}