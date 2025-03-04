using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace VertigoGames.Settings
{
    [CreateAssetMenu(fileName = "DangerRewardWindowSettings", menuName = "VertigoGames/DangerRewardWindowSettings")]
    public class DangerRewardWindowSettings : ScriptableObject
    {
        [Title("Reward Window Animation Settings")]
        [SerializeField] private float _backgroundFadeDuration = 0.1f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _rotateDuration = 0.23f;
        [SerializeField] private float _delayBeforeClose = 1f;
        [SerializeField] private float _initialCardBackRotationZ = 15;
        [SerializeField] private float _initialCardPositionY = -1500;
        [SerializeField] private float _backgroundFadeValue = .6f;
        
        #region Public References
        
        public float BackgroundFadeDuration => _backgroundFadeDuration;
        public float ScaleDuration => _scaleDuration;
        public float RotateDuration => _rotateDuration;
        public float DelayBeforeClose => _delayBeforeClose;
        public float InitialCardBackRotationZ => _initialCardBackRotationZ;
        public float InitialCardPositionY => _initialCardPositionY;
        public float BackgroundFadeValue => _backgroundFadeValue;
        
        #endregion
    }
}