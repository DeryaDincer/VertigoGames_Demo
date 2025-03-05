using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu(fileName = "DeadZoneWindowSettings", menuName = "VertigoGames/DeadZoneWindowSettings")]
    public class DeadZoneWindowSettings : ScriptableObject
    {
        [Title("Dead Zone Window Settings")]
        [SerializeField] private int _reviveGoldAmount = 25;
        [SerializeField] private string _textGoldSpriteString = "<sprite name=\"UI_icon_gold\">";
        
        [Title("Dead Zone Window Animation Settings")]
        [SerializeField] private float _backgroundFadeDuration = 0.1f;
        [SerializeField] private float _scaleDuration = 0.2f;
        [SerializeField] private float _rotateDuration = 0.23f;
        [SerializeField] private float _delayBeforeClose = 1f;
        [SerializeField] private float _initialCardBackRotationZ = 15;
        [SerializeField] private float _initialCardPositionY = -1500;
        [SerializeField] private float _backgroundFadeValue = .6f;
        
        #region Public References
        public int ReviveGoldAmount => _reviveGoldAmount;
        public string TextGoldSpriteString => _textGoldSpriteString;
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