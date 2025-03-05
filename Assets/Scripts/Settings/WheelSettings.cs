using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "WheelSettings", menuName = "VertigoGames/WheelSettings")]
    public class WheelSettings : ScriptableObject
    {
        [Title("Wheel Settings")]
        
        [SerializeField] private int _wheelSlotCountValue = 8; 
        
        [Title("Wheel Initialize Settings")]
        
        [SerializeField] private float wheelSpawnDelayBetweenItemsValueValue = .1f;
        [SerializeField] private float wheelSpawnDelayValueValue = .3f;
        
        [Title("Wheel Spin Animation Settings")]
        
        [SerializeField] private int _spinRotationCountValue;
        [SerializeField] private float _spinDurationValue;
        [SerializeField] private int _wheelRadiusValue;
        [SerializeField] private Ease _spinEaseValue;
        
        [Title("Wheel Indicator Animation Settings")]
        
        [SerializeField] private int _indicatorRotationValue;
        [SerializeField] private float _indicatorDurationValue;
        [SerializeField] private Ease _indicatorEaseValue;
        
        [Title("Wheel Scale Animation Settings")]
        
        [SerializeField] private float _wheelScaleUpValue;
        [SerializeField] private float _wheelBumpDurationValue;
        
        [Title("Wheel Appearance Settings")]
        
        [SerializeField] private List<WheelZoneAppearanceInfo> _wheelZoneAppearances;

        #region Public References
        public int WheelSlotCountValue => _wheelSlotCountValue;
        public float WheelSpawnDelayBetweenItemsValue => wheelSpawnDelayBetweenItemsValueValue;
        public float WheelSpawnDelayValue => wheelSpawnDelayValueValue;
        public int SpinRotationCountValue => _spinRotationCountValue;
        public float WheelScaleUpValue => _wheelScaleUpValue;
        public float WheelRadiusValue => _wheelRadiusValue;
        public Ease SpinEaseValue => _spinEaseValue;
        public float IndicatorRotationValue => _indicatorRotationValue;
        public float IndicatorDurationValue => _indicatorDurationValue;
        public Ease IndicatorEaseValue => _indicatorEaseValue;
        public float SpinDurationValue => _spinDurationValue;
        public float WheelBumpDurationValue => _wheelBumpDurationValue;
        public  List<WheelZoneAppearanceInfo>  WheelZoneAppearances => _wheelZoneAppearances;
        #endregion
        
        public WheelZoneAppearanceInfo GetWheelZoneAppearanceByZoneType(ZoneType zoneType)
        {
            var appearance = _wheelZoneAppearances.FirstOrDefault(wza => wza.ZoneType == zoneType);
            return appearance;
        }
    }
}