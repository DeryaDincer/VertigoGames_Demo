using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu(fileName = "ZoneBarSettings", menuName = "VertigoGames/ZoneBarSettings")]
    public class ZoneBarSettings : ScriptableObject
    {
        public int InitialIndicatorCount => _initialIndicatorCount;
        public int AverageIndicatorIndex => _averageIndicatorIndex;
        public int SlideDistance => _slideDistance;
        public float SlideDuration => _slideDuration;
        public Ease SlideEase => _slideEase;
      
        [Title("Zone Bar Settings")]
        
        [SerializeField] private int _initialIndicatorCount = 10; 
        private const int _averageIndicatorIndex = 4;
        private const int _slideDistance = 100;
        
        [Title("Zone Bar Animation Settings")]
        
        [SerializeField] private float _slideDuration = 1f;

        [SerializeField] private Ease _slideEase ;

    }
}