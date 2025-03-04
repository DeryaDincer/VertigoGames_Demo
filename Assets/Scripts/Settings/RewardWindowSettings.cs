using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace VertigoGames.Settings
{
    [CreateAssetMenu(fileName = "RewardWindowSettings", menuName = "VertigoGames/RewardWindowSettings")]
    public class RewardWindowSettings : ScriptableObject
    {
        [Title("Reward Window Animation Settings")]
        [SerializeField] private float _fadeDuration = 0.1f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _rotateDuration = 0.23f;
        [SerializeField] private float _cardFlipDuration = 0.1f;
        [SerializeField] private float _bumpScale = 1.15f;
        [SerializeField] private float _bumpDuration = 0.2f;
        [SerializeField] private float _delayBeforeClose = 1f;
        [SerializeField] private float _initialCardRotationZ = 35;
        [SerializeField] private float _initialCardBackRotationY = -65;
        [SerializeField] private float _initialCardScale = .4f;
        
        #region Public References
        
        public float FadeDuration => _fadeDuration;
        public float ScaleDuration => _scaleDuration;
        public float RotateDuration => _rotateDuration;
        public float CardFlipDuration => _cardFlipDuration;
        public float BumpScale => _bumpScale;
        public float BumpDuration => _bumpDuration;
        public float DelayBeforeClose => _delayBeforeClose;
        public float InitialCardRotationZ => _initialCardRotationZ;
        public float InitialCardBackRotationY => _initialCardBackRotationY;
        public float InitialCardScale => _initialCardScale;
        
        #endregion
    }
}