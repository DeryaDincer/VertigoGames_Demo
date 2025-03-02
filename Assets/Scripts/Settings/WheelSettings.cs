using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "WheelSettings", menuName = "VertigoGames/WheelSettings")]
    public class WheelSettings : ScriptableObject
    {
        public int WheelSlotCountValue => _wheelSlotCountValue;
        public int SpinRotationCountValue => _spinRotationCountValue;
        public float SpinDurationValue => _spinDurationValue;
        public float WheelRadiusValue => _wheelRadiusValue;
        public Ease SpinEaseValue => _spinEaseValue;
      
        [Title("Wheel Settings")]
        [SerializeField] private int _wheelSlotCountValue = 8;
        
        [Title("Wheel Animation Settings")]
        
        [SerializeField] private int _spinRotationCountValue;
        [SerializeField] private float _spinDurationValue;
        [SerializeField] private int _wheelRadiusValue;
        [SerializeField] private Ease _spinEaseValue;
    }
    
  
}