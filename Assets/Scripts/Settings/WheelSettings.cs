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
        public int RewardInfo;
    
        public int SpinRotationCountValue => spinRotationCountValue;
        public float SpinDurationValue => spinDurationValue;
        public float WheelRadiusValue => wheelRadiusValue;
        public Ease SpinEaseValue => spinEaseValue;
      
        
        [Title("Wheel Animation Settings")]
        
        [SerializeField] private int spinRotationCountValue;
        [SerializeField] private float spinDurationValue;
        [SerializeField] private int wheelRadiusValue;
        [SerializeField] private Ease spinEaseValue;
    }
    
  
}