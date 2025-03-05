using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu(fileName = "ZoneBarSettings", menuName = "VertigoGames/ZoneBarSettings")]
    public class ZoneBarSettings : ScriptableObject
    {
        [Title("Zone Bar Settings")]
        
        [SerializeField] private int _initialIndicatorCount = 10; 
        [SerializeField] private int _averageIndicatorIndex = 5;
        private readonly int _slideDistance = 100;
        
        [Title("Zone Bar Animation Settings")]
        
        [SerializeField] private float _slideDuration = 1f;
        [SerializeField] private Ease _slideEase ;
        
        [Title("Zone Bar Appearance Settings")]
        
        [SerializeField] private List<ZoneBarAppearanceInfo> _zoneBarAppearanceInfos;

        #region Public References
        public int InitialIndicatorCount => _initialIndicatorCount;
        public int AverageIndicatorIndex => _averageIndicatorIndex;
        public int SlideDistance => _slideDistance;
        public float SlideDuration => _slideDuration;
        public Ease SlideEase => _slideEase;
        #endregion
      
        public ZoneBarAppearanceInfo GetZoneBarAppearanceByZoneType(ZoneType zoneType)
        {
            var appearance = _zoneBarAppearanceInfos.FirstOrDefault(wza => wza.ZoneType == zoneType);
            return appearance;
        }

        public ZoneType GetInitialZoneType()
        {
            return ZoneType.Normal;
        }
    }
}