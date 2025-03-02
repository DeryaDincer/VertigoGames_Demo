using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "WheelSettings", menuName = "VertigoGames/WheelSettings")]
    public class WheelSettings : ScriptableObject
    {
        public int WheelSlotCountValue => _wheelSlotCountValue;
        public float WheelSpawnDelayBetweenItemsValue => wheelSpawnDelayBetweenItemsValueValue;
        public float WheelSpawnDelayValue => wheelSpawnDelayValueValue;
        public int SpinRotationCountValue => _spinRotationCountValue;
        public float SpinDurationValue => _spinDurationValue;
        public float WheelRadiusValue => _wheelRadiusValue;
        public Ease SpinEaseValue => _spinEaseValue;
      
        [Title("Wheel Settings")]
        [SerializeField] private int _wheelSlotCountValue = 8; 
        [SerializeField] private float wheelSpawnDelayBetweenItemsValueValue = .1f;
        [SerializeField] private float wheelSpawnDelayValueValue = .3f;
        
        [Title("Wheel Animation Settings")]
        
        [SerializeField] private int _spinRotationCountValue;
        [SerializeField] private float _spinDurationValue;
        [SerializeField] private int _wheelRadiusValue;
        [SerializeField] private Ease _spinEaseValue;
    }
    
  
}